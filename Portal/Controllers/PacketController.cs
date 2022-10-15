using Core.Domain.Entities;
using Core.DomainServices.Interfaces.Services;
using Core.DomainServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Portal.Models;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;

namespace Portal.Controllers
{
    public class PacketController : Controller
    {
        private readonly IPacketService _packetService;
        private readonly IProductService _productService;
        private readonly IStudentService _studentService;
        private readonly ICanteenEmployeeService _canteenEmployeeService;
        private readonly ICanteenService _canteenService;
        public PacketController(IPacketService packetService, IProductService productService, IStudentService studentService, ICanteenEmployeeService canteenEmployeeService, ICanteenService canteenService)
        {
            _packetService = packetService;
            _productService = productService;
            _studentService = studentService;
            _canteenEmployeeService = canteenEmployeeService;
            _canteenService = canteenService;
        }

        [HttpGet]
        public async Task<IActionResult> AllPackets() => View(await _packetService.GetAllAvailablePacketsAsync());

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public async Task<IActionResult> AllCanteenPackets() => View(await _packetService.GetMyCanteenOfferedPacketsAsync(User.Identity?.Name!));

        [Authorize(Policy = "StudentOnly")]
        [HttpGet]
        public async Task<IActionResult> MyReservedPackets() => View(await _packetService.GetMyReservedPacketsAsync(User.Identity?.Name!));

        [HttpGet]
        public async Task<IActionResult> Detail(int id) => View(FormatPacket(await _packetService.GetPacketByIdAsync(id)));

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public async Task<IActionResult> CreatePacket()
        {
            var model = new PacketViewModel()
            {
                AllProducts = await _productService.GetAllProductsInSelectListAsync()
            };

            return View(model);
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpPost]
        public async Task<IActionResult> CreatePacket(PacketViewModel packetViewModel)
        {
            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(User.Identity?.Name!);

            var canteen = await _canteenService.GetCanteenByLocationAsync(canteenEmployee.Location!);

            if (packetViewModel.SelectedProducts?.Count == 0)
                ModelState.AddModelError("NoProductsSelected", "Producten zijn verplicht!");

            if (packetViewModel.Packet.PickUpDateTime < DateTime.Now || packetViewModel.Packet.PickUpDateTime > packetViewModel.Packet.LatestPickUpTime)
                ModelState.AddModelError("DateNotPossible", "Deze datum en/of tijd is onmogelijk!");

            if(packetViewModel.Packet.PickUpDateTime > DateTime.Now.AddDays(2))
                ModelState.AddModelError("DateToLate", "Je mag maar maximaal 2 dagen vooruit plannen!");

            if (packetViewModel.Packet.MealType != null)
                if ((int) packetViewModel.Packet.MealType! == 4 && canteen.OfferingHotMeals == false)
                    ModelState.AddModelError("NotOfferingHotMeals", "Je kantine biedt geen warme maaltijden aan!");

            if (ModelState.IsValid)
            {
                var newPacket = await _packetService.CreatePacketAsync(packetViewModel.Packet, User.Identity?.Name!, packetViewModel.SelectedProducts!);
                return RedirectToAction("Detail", new { id = newPacket.PacketId });
            } 

            var model = new PacketViewModel()
            {
                AllProducts = await _productService.GetAllProductsInSelectListAsync(),
                SelectedProducts = packetViewModel.SelectedProducts
            };

            return View(model);
        }

        [Authorize(Policy = "StudentOnly")]
        [HttpPost]
        public async Task<IActionResult> ReservePacket(int id)
        {
            var packet = await _packetService.GetPacketByIdAsync(id);

            var student = await _studentService.GetStudentByStudentNumberAsync(User.Identity!.Name!);

            if ((bool) packet?.IsEightteenPlusPacket! && (packet?.PickUpDateTime - student?.DateOfBirth)!.Value.TotalDays < (365 * 18))
                ModelState.AddModelError("NotEighteen", "Je kan dit pakket niet reserveren, omdat dit pakket 18+ producten bevat!");

            if (packet?.ReservedBy != null)
                ModelState.AddModelError("PacketReserved", "Je kan dit pakket niet reserveren, omdat deze al gereserveerd is door een andere student!");

            if(await _packetService.CheckReservedPickUpDate(student!, packet!))
                ModelState.AddModelError("AlreadyHaveReservedPacket", "Je kan dit pakket niet reserveren, omdat je al meer dan 1 pakket op deze afhaaldatum hebt!");

            if (ModelState.IsValid)
                if ((bool) packet?.IsEightteenPlusPacket! && (packet?.PickUpDateTime - student?.DateOfBirth)!.Value.TotalDays > (365 * 18) || (bool) !packet?.IsEightteenPlusPacket!)
                    if (await _packetService.ReservePacketAsync(id, User.Identity!.Name!))
                        return RedirectToAction("MyReservedPackets", "Packet");

            return View("Detail", FormatPacket(packet!));
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public async Task<IActionResult> UpdatePacket(int id)
        {
            var packet = await _packetService.GetPacketByIdAsync(id);

            var model = new PacketViewModel()
            {
                Packet = packet,
                AllProducts = await _productService.GetAllProductsInSelectListAsync(),
                SelectedProducts = _productService.GetProductsFromPacketInList(packet)
            };

            return View(model);
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpPost]
        public async Task<IActionResult> UpdatePacket(int id, PacketViewModel packetViewModel)
        {
            var packet = await _packetService.GetPacketByIdAsync(id);

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(User.Identity?.Name!);

            var canteen = await _canteenService.GetCanteenByLocationAsync(canteenEmployee.Location!);

            if (packetViewModel.Packet.PickUpDateTime < DateTime.Now || packetViewModel.Packet.PickUpDateTime > packetViewModel.Packet.LatestPickUpTime)
                ModelState.AddModelError("DateNotPossible", "Deze datum en/of tijd is onmogelijk!");

            if (packetViewModel.SelectedProducts!.Count == 0)
                ModelState.AddModelError("NoProductsSelected", "Producten zijn verplicht!");

            if (packet.ReservedBy != null)
                ModelState.AddModelError("PacketReserved", "Je kan dit pakket niet bewerken, omdat deze al gereserveerd is!");

            if(packetViewModel.Packet.MealType != null)
                if ((int)packetViewModel.Packet.MealType! == 4 && canteen.OfferingHotMeals == false)
                    ModelState.AddModelError("NotOfferingHotMeals", "Je kantine biedt geen warme maaltijden aan!");

            if (ModelState.IsValid)
                if (await _packetService.UpdatePacketAsync(id, packetViewModel.Packet, User.Identity?.Name!, packetViewModel.SelectedProducts))
                    return RedirectToAction("Detail", new { id });

            var model = new PacketViewModel()
            {
                Packet = packet,
                AllProducts = await _productService.GetAllProductsInSelectListAsync(),
                SelectedProducts = _productService.GetProductsFromPacketInList(packet)
            };

            return View(model);
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpPost]
        public async Task<IActionResult> DeletePacket(int id)
        {
            var packet = await _packetService.GetPacketByIdAsync(id);

            if (packet?.ReservedBy != null)
                ModelState.AddModelError("PacketReserved", "Je kan dit pakket niet verwijderen, omdat deze al gereserveerd is!");

            if (ModelState.IsValid)
                if (await _packetService.DeletePacketAsync(id, User.Identity!.Name!))
                    return RedirectToAction("Index", "Home");

            return View("Detail", FormatPacket(packet!));
        }

        public PacketDetailViewModel FormatPacket(Packet packet)
        {
            var products = new List<ProductViewModel>();

            foreach (var product in packet.Products!)
            {
                var productModel = new ProductViewModel()
                {
                    Name = product.Name,
                    IsAlcoholic = ((bool)product.IsAlcoholic!) ? "Ja" : "Nee",
                    Picture = @String.Format("data:image/png;base64,{0}", Convert.ToBase64String(product.Picture!))
                };

                products.Add(productModel);
            }

            var packetModel = new PacketDetailViewModel()
            {
                PacketId = packet.PacketId,
                Name = packet.Name,
                Products = products,
                City = packet.City!.GetType().GetMember(packet.City.ToString()!).First().GetCustomAttribute<DisplayAttribute>()!.GetName(),
                Canteen = packet.Canteen!.Location,
                PickUpDateTime = new DateTime(packet.PickUpDateTime!.Value.Year, packet.PickUpDateTime!.Value.Month, packet.PickUpDateTime!.Value.Day, packet.PickUpDateTime!.Value.Hour, packet.PickUpDateTime!.Value.Minute, packet.PickUpDateTime!.Value.Second).ToString("dddd d MMMM yyyy HH:mm", new CultureInfo("nl-NL")),
                LatestPickUpTime = new DateTime(packet.LatestPickUpTime!.Value.Year, packet.LatestPickUpTime!.Value.Month, packet.LatestPickUpTime!.Value.Day, packet.LatestPickUpTime!.Value.Hour, packet.LatestPickUpTime!.Value.Minute, packet.LatestPickUpTime!.Value.Second).ToString("dddd d MMMM yyyy HH:mm", new CultureInfo("nl-NL")),
                IsEightteenPlusPacket = ((bool)packet.IsEightteenPlusPacket!) ? "(18+)" : "",
                Price = String.Format("{0:€#,##0.00}", packet.Price),
                MealType = packet.MealType!.GetType().GetMember(packet.MealType.ToString()!).First().GetCustomAttribute<DisplayAttribute>()!.GetName(),
                ReservedBy = (packet.ReservedBy != null) ? packet.ReservedBy.Name : "-"
            };

            ViewData["UserName"] = packet.ReservedBy?.Name;

            return packetModel;
        }
    }
}
