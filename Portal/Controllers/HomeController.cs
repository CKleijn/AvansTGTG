using Core.DomainServices.Interfaces.Services;
using Core.DomainServices.Services;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPacketService _packetService;

        public HomeController(IPacketService packetService)
        {
            _packetService = packetService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = User.Identity?.Name!;
            var isStudent = User.HasClaim("Role", "Student");
            var isCanteenEmployee = User.HasClaim("Role", "CanteenEmployee");

            var allAvailablePackets = await _packetService.GetAllAvailablePacketsAsync();

            var model = new HomeViewModel()
            {
                Student = isStudent,
                CanteenEmployee = isCanteenEmployee,
                AllPackets = allAvailablePackets.Take(4)
            };

            if (user != null && isCanteenEmployee)
            {
                var allCanteenPackets = await _packetService.GetMyCanteenOfferedPacketsAsync(user);
                model.AllCanteenPackets = allCanteenPackets.Take(4);
            }

            if (user != null && isStudent)
            {
                var myReservedPackets = await _packetService.GetMyReservedPacketsAsync(user);
                model.MyReservedPackets = myReservedPackets.Take(4);
            }

            return View(model);
        }
    }
}
