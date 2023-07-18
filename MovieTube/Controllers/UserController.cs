using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieTube.Repositories;

namespace MovieTube.Controllers
{
    public class UserController : Controller
    {
        private readonly IIdentityUserRepository _user;

        public UserController(IIdentityUserRepository user)
        {
            _user = user;
        }


        [HttpGet]
        public ActionResult Register()
        {
            RegisterUserViewModel user = new();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserViewModel user)
        {
            var result = await _user.RegisterUserAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    
                    ModelState.AddModelError("RegisterError", error.Description);
                }
                return View("Register", user);
            }
            else
            {
                return RedirectToAction("ViewAllMovies", "UserMovies");
            }
        }

        [HttpGet]
        public ActionResult Login() 
        {
            UserLogInViewModel user = new();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogInViewModel login) 
        {
            UserWapper userwrapper = new();
            userwrapper = await _user.UserLoginAsync(login);
            if (userwrapper.signInResult.IsNotAllowed)
            {
                return View("Login", login);
            }
            //CREATE ROLE USER & ADMIN OTHERWISE => EXCEPTION >>> ROLE NOT FOUND 
            bool isInUserRole= await _user.FindUserRoleAsync(userwrapper.user);
            
            return RedirectToAction("ViewAllMovies", "UserMovies");

            //if (isInUserRole)
            //{
            //}

            //TODO

            //else
            //{
            //    return RedirectToAction(getadminpanel);
            //}

        }

        [HttpGet]
        public async Task Logout() 
        {
           await _user.UserLogoutAsync();
        }

        public async Task<IActionResult> CheckUserAvilablity(string UserName)
        {
            var FoundUser = await _user.FindUserByNameAsync(UserName);
            if (FoundUser is null) 
            {
                return Json(true);
            }
            else 
            { 
                return Json(false); 
            }

        }

        public async Task<IActionResult> CheckUserAvilablityByEmail(string Email)
        {
            string normalizedname = Email.ToUpper();
            var FoundUser = await _user.FindUserByEmailAsync(normalizedname);
            if (FoundUser is null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }


    }
}
