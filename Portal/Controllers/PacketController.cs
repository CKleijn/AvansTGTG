﻿namespace Portal.Controllers
{
    public class PacketController : Controller
    {
        private readonly IPacketService _packetService;
        private readonly IProductService _productService;
        private readonly IStudentService _studentService;
        public PacketController(IPacketService packetService, IProductService productService, IStudentService studentService)
        {
            _packetService = packetService;
            _productService = productService;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var allPackets = await GetPacketListViewModelAsync(await _packetService.GetAllAvailablePacketsAsync());

            var model = new PacketOverviewViewModel()
            {
                AllPackets = allPackets.PacketList!.Take(4)
            };

            if (User.HasClaim("Role", "CanteenEmployee"))
            {
                var allCanteenPackets = await GetPacketListViewModelAsync(await _packetService.GetMyCanteenOfferedPacketsAsync(User.Identity?.Name!));
                model.AllCanteenPackets = allCanteenPackets.PacketList!.Take(4);
            }

            if (User.HasClaim("Role", "Student"))
            {
                var myReservedPackets = await GetPacketListViewModelAsync(await _packetService.GetMyReservedPacketsAsync(User.Identity?.Name!));
                model.MyReservedPackets = myReservedPackets.PacketList!.Take(4);
            }

            return View(model);
        }

        public async Task<IActionResult> AllPackets() => View(await GetPacketListViewModelAsync(await _packetService.GetAllAvailablePacketsAsync()));

        [Authorize(Policy = "CanteenEmployeeOnly")]
        public async Task<IActionResult> MyCanteenPackets() => View(await GetPacketListViewModelAsync(await _packetService.GetMyCanteenOfferedPacketsAsync(User.Identity?.Name!)));

        [Authorize(Policy = "CanteenEmployeeOnly")]
        public async Task<IActionResult> OtherCanteenPackets() => View(await GetPacketListViewModelAsync(await _packetService.GetOtherCanteenOfferedPacketsAsync(User.Identity?.Name!)));

        [Authorize(Policy = "StudentOnly")]
        public async Task<IActionResult> MyReservedPackets() => View(await GetPacketListViewModelAsync(await _packetService.GetMyReservedPacketsAsync(User.Identity?.Name!)));

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                return View(await GetPacketDetailViewModelAsync(await _packetService.GetPacketByIdAsync(id)));
            }
            catch (Exception e)
            {
                if (e.Message == "Er bestaat geen pakket met dit ID!")
                    ModelState.AddModelError("PacketNotFound", e.Message);

                return RedirectToAction("Index", "Packet");
            }
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
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreatePacket(PacketViewModel packetViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newPacket = await _packetService.CreatePacketAsync(packetViewModel.Packet, User.Identity?.Name!, packetViewModel.SelectedProducts!);
                    return RedirectToAction("Detail", new { id = newPacket.PacketId });
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                if (e.Message == "Producten zijn verplicht!")
                    ModelState.AddModelError("NoProductsSelected", e.Message);

                if (e.Message == "Deze datum en/of tijd is onmogelijk!")
                    ModelState.AddModelError("DateNotPossible", e.Message);

                if (e.Message == "Je mag maar maximaal 2 dagen vooruit plannen!")
                    ModelState.AddModelError("DateToLate", e.Message);

                if (e.Message == "De uiterlijke afhaaltijd moet plaatsvinden op dezelfde dag als de ophaaldag!")
                    ModelState.AddModelError("DateOnOtherDay", e.Message);

                if (e.Message == "Je kantine biedt geen warme maaltijden aan!")
                    ModelState.AddModelError("NotOfferingHotMeals", e.Message);

                if (e.Message == "Er bestaat geen product met deze naam!")
                    ModelState.AddModelError("ProductNotFound", e.Message);

                if (e.Message == "Het pakket is niet aangemaakt!")
                    ModelState.AddModelError("PacketNotCreated", e.Message);

                var model = new PacketViewModel()
                {
                    AllProducts = await _productService.GetAllProductsInSelectListAsync(),
                    SelectedProducts = packetViewModel.SelectedProducts
                };

                return View(model);
            }
        }

        [Authorize(Policy = "StudentOnly")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ReservePacket(int id)
        {
            try
            {
                if (ModelState.IsValid)
                    if (await _packetService.ReservePacketAsync(id, User.Identity?.Name!))
                        return RedirectToAction("MyReservedPackets", "Packet");

                throw new Exception();
            }
            catch (Exception e)
            {
                if (e.Message == "Er bestaat geen pakket met dit ID!")
                {
                    ModelState.AddModelError("PacketNotFound", e.Message);
                    return RedirectToAction("Index", "Packet");
                }

                if (e.Message == "Je kan dit pakket niet reserveren, omdat dit pakket 18+ producten bevat!")
                    ModelState.AddModelError("NotEightteen", e.Message);

                if (e.Message == "Je kan dit pakket niet reserveren, omdat deze al gereserveerd is door een andere student!")
                    ModelState.AddModelError("PacketReservedByOtherStudent", e.Message);

                if (e.Message == "Je kan dit pakket niet reserveren, omdat je al meer dan 1 pakket op deze afhaaldatum hebt!")
                    ModelState.AddModelError("AlreadyHaveReservedPacket", e.Message);

                if (e.Message == "Het pakket is niet gereserveerd!")
                    ModelState.AddModelError("PacketNotReserved", e.Message);

                return View("Detail", await GetPacketDetailViewModelAsync(await _packetService.GetPacketByIdAsync(id)));
            }
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public async Task<IActionResult> UpdatePacket(int id) {
            try
            {
                return View(await GetPacketViewModelAsync(await _packetService.GetPacketByIdAsync(id)));
            }
            catch (Exception e)
            {
                if (e.Message == "Er bestaat geen pakket met dit ID!")
                    ModelState.AddModelError("PacketNotFound", e.Message);

                return RedirectToAction("Index", "Packet");
            }
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdatePacket(int id, PacketViewModel packetViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                    if (await _packetService.UpdatePacketAsync(id, packetViewModel.Packet, User.Identity?.Name!, packetViewModel.SelectedProducts!))
                        return RedirectToAction("Detail", new { id });

                throw new Exception();
            }
            catch (Exception e)
            {
                if (e.Message == "Er bestaat geen pakket met dit ID!")
                {
                    ModelState.AddModelError("PacketNotFound", e.Message);
                    return RedirectToAction("Index", "Packet");
                }

                if (e.Message == "Dit pakket is niet van jouw kantine!")
                    ModelState.AddModelError("NoCanteenPacket", e.Message);

                if (e.Message == "Producten zijn verplicht!")
                    ModelState.AddModelError("NoProductsSelected", e.Message);

                if (e.Message == "Deze datum en/of tijd is onmogelijk!")
                    ModelState.AddModelError("DateNotPossible", e.Message);

                if (e.Message == "Je mag maar maximaal 2 dagen vooruit plannen!")
                    ModelState.AddModelError("DateToLate", e.Message);

                if (e.Message == "De uiterlijke afhaaltijd moet plaatsvinden op dezelfde dag als de ophaaldag!")
                    ModelState.AddModelError("DateOnOtherDay", e.Message);

                if (e.Message == "Je kan dit pakket niet bewerken, omdat deze al gereserveerd is!")
                    ModelState.AddModelError("PacketAlreadyReserved", e.Message);

                if (e.Message == "Je kantine biedt geen warme maaltijden aan!")
                    ModelState.AddModelError("NotOfferingHotMeals", e.Message);

                if (e.Message == "Er bestaat geen product met deze naam!")
                    ModelState.AddModelError("ProductNotFound", e.Message);

                if (e.Message == "Het pakket is niet bewerkt!")
                    ModelState.AddModelError("PacketNotUpdated", e.Message);

                return View(await GetPacketViewModelAsync(await _packetService.GetPacketByIdAsync(id)));
            }
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeletePacket(int id)
        {
            try
            {
                if (ModelState.IsValid)
                    if (await _packetService.DeletePacketAsync(id, User.Identity?.Name!))
                        return RedirectToAction("Index", "Packet");

                throw new Exception();
            }
            catch (Exception e)
            {
                if (e.Message == "Er bestaat geen pakket met dit ID!")
                {
                    ModelState.AddModelError("PacketNotFound", e.Message);
                    return RedirectToAction("Index", "Packet");
                }

                if (e.Message == "Dit pakket is niet van jouw kantine!")
                    ModelState.AddModelError("NoCanteenPacket", e.Message);

                if (e.Message == "Je kan dit pakket niet verwijderen, omdat deze al gereserveerd is!")
                    ModelState.AddModelError("PacketAlreadyReserved", e.Message);

                if (e.Message == "Het pakket is niet verwijderd!")
                    ModelState.AddModelError("PacketNotDeleted", e.Message);

                return View("Detail", await GetPacketDetailViewModelAsync(await _packetService.GetPacketByIdAsync(id)));
            }
        }

        public async Task<PacketViewModel> GetPacketViewModelAsync(Packet packet)
        {
            return new PacketViewModel()
            {
                Packet = packet,
                AllProducts = await _productService.GetAllProductsInSelectListAsync(),
                SelectedProducts = _productService.GetProductsNamesFromPacketInList(packet)
            };
        }

        public async Task<PacketDetailViewModel> GetPacketDetailViewModelAsync(Packet packet) => await FormatPacketAsync(packet);

        public async Task<PacketListViewModel> GetPacketListViewModelAsync(IEnumerable<Packet> packets)
        {
            var listOfFormattedPackets = new List<PacketDetailViewModel>();

            foreach (var packet in packets)
                listOfFormattedPackets.Add(await FormatPacketAsync(packet));

            return new PacketListViewModel()
            {
                PacketList = listOfFormattedPackets
            };
        }

        public async Task<PacketDetailViewModel> FormatPacketAsync(Packet packet)
        {
            Student? student = null;

            try
            {
                if(User.HasClaim("Role", "Student"))
                    student = await _studentService.GetStudentByStudentNumberAsync(User.Identity?.Name!);
            }
            catch (Exception e)
            {
                if (e.Message == "Er bestaat geen student met dit studentennummer!")
                    student = null;
            }

            return new PacketDetailViewModel()
            {
                PacketId = packet.PacketId,
                Name = packet.Name,
                Products = packet.Products?.FormatProduct(),
                City = packet.GetCity(),
                Canteen = packet.GetCanteen(),
                PickUpDateTime = packet.GetPickUpDateTime(),
                LatestPickUpTime = packet.GetLatestPickUpTime(),
                IsEightteenPlusPacket = packet.GetIsEightteenPlusPacket(),
                Price = packet.GetPrice(),
                MealType = packet.GetMealType(),
                ReservedBy = packet.GetReservedBy(),
                StudentName = student?.Name
            };
        }
    }
}
