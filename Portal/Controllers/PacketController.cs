using Core.Domain.Enums;
using Core.DomainServices.Interfaces.Services;
using Portal.Models.AccountVM;
using Portal.Models.PacketVM;
using Portal.Models.ProductVM;
using System.Globalization;
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

        public UserViewModel FillUser()
        {
            return new UserViewModel()
            {
                UserName = User.Identity?.Name!,
                IsStudent = User.HasClaim("Role", "Student"),
                IsCanteenEmployee = User.HasClaim("Role", "CanteenEmployee")
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = FillUser();

            var allPackets = await GetPacketListViewModelAsync(await _packetService.GetAllAvailablePacketsAsync());

            var model = new PacketOverviewViewModel()
            {
                User = user,
                AllPackets = allPackets.PacketList!.Take(4)
            };

            if (user.UserName != null && user.IsCanteenEmployee)
            {
                var allCanteenPackets = await GetPacketListViewModelAsync(await _packetService.GetMyCanteenOfferedPacketsAsync(user.UserName));
                model.AllCanteenPackets = allCanteenPackets.PacketList!.Take(4);
            }

            if (user.UserName != null && user.IsStudent)
            {
                var myReservedPackets = await GetPacketListViewModelAsync(await _packetService.GetMyReservedPacketsAsync(user.UserName));
                model.MyReservedPackets = myReservedPackets.PacketList!.Take(4);
            }

            return View(model);
        }

        public async Task<IActionResult> AllPackets() => View(await GetPacketListViewModelAsync(await _packetService.GetAllAvailablePacketsAsync()));

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public async Task<IActionResult> AllCanteenPackets() => View(await GetPacketListViewModelAsync(await _packetService.GetMyCanteenOfferedPacketsAsync(FillUser().UserName!)));

        [Authorize(Policy = "StudentOnly")]
        [HttpGet]
        public async Task<IActionResult> MyReservedPackets() => View(await GetPacketListViewModelAsync(await _packetService.GetMyReservedPacketsAsync(FillUser().UserName!)));

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var packet = await _packetService.GetPacketByIdAsync(id);

            return View(await GetPacketDetailViewModelAsync(packet));
        }

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
            var user = FillUser();

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(user.UserName!);

            var canteen = await _canteenService.GetCanteenByLocationAsync((Location) canteenEmployee.Location!);

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
                var newPacket = await _packetService.CreatePacketAsync(packetViewModel.Packet, user.UserName!, packetViewModel.SelectedProducts!);
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
            var user = FillUser();

            var packet = await _packetService.GetPacketByIdAsync(id);

            var student = await _studentService.GetStudentByStudentNumberAsync(user.UserName!);

            if ((bool) packet?.IsEightteenPlusPacket! && student.DateOfBirth!.Value.AddYears(18) > packet.PickUpDateTime)
                ModelState.AddModelError("NotEighteen", "Je kan dit pakket niet reserveren, omdat dit pakket 18+ producten bevat!");

            if (packet?.ReservedBy != null)
                ModelState.AddModelError("PacketReserved", "Je kan dit pakket niet reserveren, omdat deze al gereserveerd is door een andere student!");

            if(await _packetService.CheckReservedPickUpDate(student!, packet!))
                ModelState.AddModelError("AlreadyHaveReservedPacket", "Je kan dit pakket niet reserveren, omdat je al meer dan 1 pakket op deze afhaaldatum hebt!");

            if (ModelState.IsValid)
                if ((bool) packet?.IsEightteenPlusPacket! && (packet?.PickUpDateTime - student?.DateOfBirth)!.Value.TotalDays > (365 * 18) || (bool) !packet?.IsEightteenPlusPacket!)
                    if (await _packetService.ReservePacketAsync(id, user.UserName!))
                        return RedirectToAction("MyReservedPackets", "Packet");

            return View("Detail", await GetPacketDetailViewModelAsync(packet!));
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public async Task<IActionResult> UpdatePacket(int id)
        {
            var user = FillUser();

            var packet = await _packetService.GetPacketByIdAsync(id);

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(user.UserName!);

            if(packet.Canteen?.Location != canteenEmployee.Location)
                return RedirectToAction("Index", "Packet");

            return View(await GetPacketViewModelAsync(packet));
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpPost]
        public async Task<IActionResult> UpdatePacket(int id, PacketViewModel packetViewModel)
        {
            var user = FillUser();

            var packet = await _packetService.GetPacketByIdAsync(id);

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(user.UserName!);

            var canteen = await _canteenService.GetCanteenByLocationAsync((Location) canteenEmployee.Location!);

            if (packet.Canteen?.Location != canteenEmployee.Location)
                return RedirectToAction("Index", "Packet");

            if (packetViewModel.Packet.PickUpDateTime < DateTime.Now || packetViewModel.Packet.PickUpDateTime > packetViewModel.Packet.LatestPickUpTime)
                ModelState.AddModelError("DateNotPossible", "Deze datum en/of tijd is onmogelijk!");

            if(packetViewModel.Packet.PickUpDateTime > DateTime.Now.AddDays(2))
                ModelState.AddModelError("DateToLate", "Je mag maar maximaal 2 dagen vooruit plannen!");

            if (packetViewModel.SelectedProducts!.Count == 0)
                ModelState.AddModelError("NoProductsSelected", "Producten zijn verplicht!");

            if (packet.ReservedBy != null)
                ModelState.AddModelError("PacketReserved", "Je kan dit pakket niet bewerken, omdat deze al gereserveerd is!");

            if(packetViewModel.Packet.MealType != null)
                if ((int)packetViewModel.Packet.MealType! == 4 && canteen.OfferingHotMeals == false)
                    ModelState.AddModelError("NotOfferingHotMeals", "Je kantine biedt geen warme maaltijden aan!");

            if (ModelState.IsValid)
                if (await _packetService.UpdatePacketAsync(id, packetViewModel.Packet, user.UserName!, packetViewModel.SelectedProducts))
                    return RedirectToAction("Detail", new { id });

            return View(await GetPacketViewModelAsync(packet));
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpPost]
        public async Task<IActionResult> DeletePacket(int id)
        {
            var user = FillUser();

            var packet = await _packetService.GetPacketByIdAsync(id);

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(user.UserName!);

            if (packet.Canteen?.Location != canteenEmployee.Location)
                return RedirectToAction("Index", "Packet");

            if (packet?.ReservedBy != null)
                ModelState.AddModelError("PacketReserved", "Je kan dit pakket niet verwijderen, omdat deze al gereserveerd is!");

            if (ModelState.IsValid)
                if (await _packetService.DeletePacketAsync(id, user.UserName!))
                    return RedirectToAction("Index", "Packet");

            return View("Detail", await GetPacketDetailViewModelAsync(packet!));
        }

        public async Task<PacketViewModel> GetPacketViewModelAsync(Packet packet)
        {
            return new PacketViewModel()
            {
                Packet = packet,
                AllProducts = await _productService.GetAllProductsInSelectListAsync(),
                SelectedProducts = _productService.GetProductsFromPacketInList(packet)
            };
        }

        public async Task<PacketDetailViewModel> GetPacketDetailViewModelAsync(Packet packet)
        {
            var user = FillUser();

            var formattedPacket = await FormatPacketAsync(packet);

            formattedPacket.User = user;
            formattedPacket.User.IsCanteenLocation = User.HasClaim("Location", packet.Canteen?.Location.ToString()!);

            return formattedPacket;
        }

        public async Task<PacketListViewModel> GetPacketListViewModelAsync(IEnumerable<Packet> packets)
        {
            var listOfFormattedPackets = new List<PacketDetailViewModel>();

            foreach (var packet in packets)
                listOfFormattedPackets.Add(await FormatPacketAsync(packet));

            return new PacketListViewModel()
            {
                User = FillUser(),
                PacketList = listOfFormattedPackets
            };
        }

        public List<ProductViewModel> FormatProduct(ICollection<Product> products)
        {
            var productList = new List<ProductViewModel>();

            foreach (var product in products!)
            {
                var productModel = new ProductViewModel()
                {
                    Name = product.Name,
                    IsAlcoholic = ((bool)product.IsAlcoholic!) ? "Ja" : "Nee",
                    Picture = @String.Format("data:image/png;base64,{0}", Convert.ToBase64String(product.Picture!))
                };

                productList.Add(productModel);
            }

            return productList;
        }

        public async Task<PacketDetailViewModel> FormatPacketAsync(Packet packet)
        {
            var user = FillUser();

            var student = user.IsStudent ? await _studentService.GetStudentByStudentNumberAsync(user.UserName!) : null;

            var products = FormatProduct(packet.Products!);

            var packetModel = new PacketDetailViewModel()
            {
                PacketId = packet.PacketId,
                Name = packet.Name,
                Products = products,
                City = packet.City?.GetType().GetMember(packet.City.ToString()!).First().GetCustomAttribute<DisplayAttribute>()!.GetName(),
                Canteen = packet.Canteen?.Location.ToString(),
                PickUpDateTime = new DateTime(packet.PickUpDateTime!.Value.Year, packet.PickUpDateTime!.Value.Month, packet.PickUpDateTime!.Value.Day, packet.PickUpDateTime!.Value.Hour, packet.PickUpDateTime!.Value.Minute, packet.PickUpDateTime!.Value.Second).ToString("dddd d MMMM yyyy HH:mm", new CultureInfo("nl-NL")),
                LatestPickUpTime = new DateTime(packet.LatestPickUpTime!.Value.Year, packet.LatestPickUpTime!.Value.Month, packet.LatestPickUpTime!.Value.Day, packet.LatestPickUpTime!.Value.Hour, packet.LatestPickUpTime!.Value.Minute, packet.LatestPickUpTime!.Value.Second).ToString("dddd d MMMM yyyy HH:mm", new CultureInfo("nl-NL")),
                IsEightteenPlusPacket = ((bool)packet.IsEightteenPlusPacket!) ? "(18+)" : "",
                Price = String.Format("{0:€#,##0.00}", packet.Price),
                MealType = packet.MealType?.GetType().GetMember(packet.MealType.ToString()!).First().GetCustomAttribute<DisplayAttribute>()!.GetName(),
                ReservedBy = (packet.ReservedBy != null) ? packet.ReservedBy.Name : "-",
                StudentName = student?.Name
            };

            return packetModel;
        }
    }
}
