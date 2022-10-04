namespace Portal.Controllers
{
    //[Authorize(Policy = "StudentOnly")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
