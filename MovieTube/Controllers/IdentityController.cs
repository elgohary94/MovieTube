using Microsoft.AspNetCore.Mvc;

namespace MovieTube.Controllers
{
    public class IdentityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
