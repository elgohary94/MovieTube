using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieTube.Repositories;

namespace MovieTube.Controllers
{
    public class UserController : Controller
    {
        private readonly IIdentityUserRepository _user;
        private readonly IIdentityRoleRepository _role;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(IIdentityUserRepository user,IIdentityRoleRepository role, SignInManager<IdentityUser> signInManager)
        {
            _user = user;
            _role = role;
            _signInManager = signInManager;
        }


        [HttpGet]
        public ActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RegisterUserViewModel user = new();
                return View(user);
            }
            else
            {
                return RedirectToAction("ViewAllMovies", "Movies");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserViewModel user)
        {
            
            var result = await _user.RegisterUserAsync(user,"User");
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
                return RedirectToAction("ViewAllMovies", "Movies");
            }
        }

        [HttpGet]
        public ActionResult Login() 
        {
            if (!User.Identity.IsAuthenticated)
            {
                UserLogInViewModel user = new();
                return View(user);
            }
            else
            {
                return RedirectToAction("ViewAllMovies", "Movies");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogInViewModel login) 
        {
            try
            {

                UserWrapper userwrapper = new();
                userwrapper = await _user.UserLoginAsync(login);
                var userRole = await  _role.FindUserRoleAsync(userwrapper.user);
                
                foreach (var item in userRole)
                {
                    if (item != "Admin")
                    {
                        return RedirectToAction("ViewAllMovies", "Movies");

                    }
                    
                }
                //TODO => admin panel and redirect ro it 
                return RedirectToAction("GetAllRoles", "RoleManage");
                


            }
            catch (Exception)
            {
                ModelState.AddModelError("log in error", "User Name Or Password Is Incorrect!");
                return View("Login", login);

            }
            

        }

        [HttpGet]
        public async Task<IActionResult> Logout() 
        {
            if (User.Identity.IsAuthenticated)
            {
                await _user.UserLogoutAsync();
                return RedirectToAction("ViewAllMovies", "Movies");
            }
            else
            {
                return View("Login");
            }
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
