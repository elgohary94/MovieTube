using Microsoft.AspNetCore.Mvc;
using MovieTube.Repositories;

namespace MovieTube.Controllers;

public class RoleController : Controller
{
    private readonly IIdentityRoleRepository _identityRole;

    public RoleController(IIdentityRoleRepository identityRole)
    {
        _identityRole = identityRole;
    }

    [HttpGet]
    public async Task<IActionResult> Role()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Role(NewRoleViewModel roleViewModel)
    {
        var createdRole = await _identityRole.CreateRole(roleViewModel);
        if (createdRole.Succeeded)
        {
            //go to admin panel 
            return RedirectToAction("AllRoles");
        }
        else
        {
            return RedirectToAction("Role");
        }

    }

    [HttpGet]
    public IActionResult AllRoles()
    {
        var roles = _identityRole.GetallRoles(); 
        return View(roles);
    }
}