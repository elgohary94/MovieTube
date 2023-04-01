using Microsoft.AspNetCore.Mvc;

namespace MovieTube.Controllers
{
    public class testController : Controller
    {
        public IActionResult hi()
        {
            return Content("hiiiiiiiii");
        }
    }
}
