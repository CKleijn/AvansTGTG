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

        public async Task<IActionResult> Index()
        {
            var user = User.Identity?.Name;
            var isStudent = User.HasClaim("Role", "Student");
            var isCanteenEmployee = User.HasClaim("Role", "CanteenEmployee");

            var model = new HomeViewModel()
            {
                Student = isStudent,
                CanteenEmployee = isCanteenEmployee,
                AllPackets = await _packetService.GetPacketsAsync()
            };

            if(user != null && isCanteenEmployee)
            {
                model.AllCanteenPackets = await _packetService.GetMyCanteenOfferedPacketsAsync(user);
            }

            if (user != null && isStudent)
            {
                model.MyReservedPackets = await _packetService.GetMyReservedPacketsAsync(user);
            }

            return View(model);
        }
    }
}
