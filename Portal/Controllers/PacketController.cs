using Core.Domain.Entities;
using Core.DomainServices.Interfaces.Services;
using Core.DomainServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Models;

namespace Portal.Controllers
{
    public class PacketController : Controller
    {
        private readonly IPacketService _packetService;
        private readonly IProductService _productService;
        public PacketController(IPacketService packetService, IProductService productService)
        {
            _packetService = packetService;
            _productService = productService;
        }
        public async Task<IActionResult> AllPackets()
        {
            return View(await _packetService.GetPacketsAsync());
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        public async Task<IActionResult> AllCanteenPackets()
        {
            var user = User.Identity?.Name;

            if(user != null)
            {
                return View(await _packetService.GetMyCanteenOfferedPacketsAsync(user));
            }

            return View();
        }

        [Authorize(Policy = "StudentOnly")]
        public async Task<IActionResult> MyReservedPackets()
        {
            var user = User.Identity?.Name;

            if (user != null)
            {
                return View(await _packetService.GetMyReservedPacketsAsync(user));
            }

            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _packetService.GetPacketByIdAsync(id));
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public async Task<IActionResult> CreatePacket()
        {
            var model = new PacketViewModel()
            {
                AllProducts = await GetAllProducts()
            };

            return View(model);
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpPost]
        public async Task<IActionResult> CreatePacket(PacketViewModel packetViewModel)
        {
            var user = User.Identity?.Name;

            if (user != null)
            {
                if(packetViewModel.Packet.PickUpDateTime < DateTime.Now || packetViewModel.Packet.PickUpDateTime > packetViewModel.Packet.LatestPickUpTime)
                {
                    ModelState.AddModelError("DateNotPossible", "Deze datum en/of tijd is onmogelijk!");
                }

                if (packetViewModel.SelectedProducts.Count == 0)
                {
                    ModelState.AddModelError("NoProductsSelected", "Producten zijn verplicht!");
                }

                if (ModelState.IsValid)
                {
                    var newPacket = await _packetService.CreatePacketAsync(packetViewModel.Packet, user, packetViewModel.SelectedProducts);

                    if (newPacket != null)
                    {
                        return RedirectToAction("Detail", new { id = newPacket.PacketId });
                    }
                } 
                else
                {
                    var model = new PacketViewModel()
                    {
                        AllProducts = await GetAllProducts(),
                        SelectedProducts = packetViewModel.SelectedProducts
                    };

                    return View(model);
                }
            }

            return View();
        }

        [Authorize(Policy = "StudentOnly")]
        [HttpPost]
        public async Task<IActionResult> ReservePacket(int id)
        {
            var user = User.Identity?.Name;

            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    var packet = await _packetService.GetPacketByIdAsync(id);

                    if(packet != null)
                    {
                        if (packet.ReservedBy == null)
                        {
                            var isReserved = await _packetService.ReservePacketAsync(id, user);

                            if (isReserved)
                            {
                                return RedirectToAction("MyReservedPackets", "Packet");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("PacketReserved", "Je kan dit pakket niet reserveren, omdat deze al gereserveerd is door een andere student!");
                        }
                    }
                }
            }

            return RedirectToAction("Detail", new { id = id });
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public async Task<IActionResult> UpdatePacket(int id)
        {
            var packet = await _packetService.GetPacketByIdAsync(id);

            var products = new List<string>();

            foreach(var product in packet.Products)
            {
                products.Add(product.Name);
            }

            var model = new PacketViewModel()
            {
                Packet = packet,
                AllProducts = await GetAllProducts(),
                SelectedProducts = products
            };

            return View(model);
        }

        //[Authorize(Policy = "CanteenEmployeeOnly")]
        //[HttpPost]
        //public async Task<IActionResult> UpdatePacket(int id, PacketViewModel packetViewModel)
        //{
        //    var user = User.Identity?.Name;

        //    if (user != null)
        //    {
        //        if (packetViewModel.Packet.PickUpDateTime < DateTime.Now || packetViewModel.Packet.PickUpDateTime > packetViewModel.Packet.LatestPickUpTime)
        //        {
        //            ModelState.AddModelError("DateNotPossible", "Deze datum en/of tijd is onmogelijk!");
        //        }

        //        if (packetViewModel.SelectedProducts.Count == 0)
        //        {
        //            ModelState.AddModelError("NoProductsSelected", "Producten zijn verplicht!");
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            var packet = await _packetService.GetPacketByIdAsync(id);

        //            if (packet.ReservedBy == null)
        //            {
        //                var isUpdated = await _packetService.UpdatePacketAsync(id, user, packetViewModel.Packet, packetViewModel.SelectedProducts);

        //                if (isUpdated)
        //                {
        //                    return RedirectToAction("Detail", new { id = id });
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("PacketReserved", "Je kan dit pakket niet bewerken, omdat deze al gereserveerd is!");
        //            }
        //        }
        //    }

        //    var model = new PacketViewModel()
        //    {
        //        Packet = packetViewModel.Packet,
        //        AllProducts = await GetAllProducts(),
        //        SelectedProducts = packetViewModel.SelectedProducts
        //    };

        //    return View(model);
        //}

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpPost]
        public async Task<IActionResult> DeletePacket(int id)
        {
            var user = User.Identity?.Name;

            if (user != null)
            {
                if(ModelState.IsValid)
                {
                    var packet = await _packetService.GetPacketByIdAsync(id);
                    
                    if(packet.ReservedBy == null)
                    {
                        var isDeleted = await _packetService.DeletePacketAsync(id, user);

                        if (isDeleted)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("PacketReserved", "Je kan dit pakket niet verwijderen, omdat deze al gereserveerd is!");
                    }
                }
            }

            return RedirectToAction("Detail", new { id = id });
        }

        private async Task<List<SelectListItem>> GetAllProducts()
        {
            var products = new List<SelectListItem>();

            foreach (var product in await _productService.GetProductsAsync())
            {
                products.Add(new SelectListItem { Text = product.Name, Value = product.Name });
            }

            return products;
        }
    }
}
