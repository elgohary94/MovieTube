using Microsoft.AspNetCore.Mvc;
using MovieTube.Repositories;

namespace MovieTube.Controllers;

public class RoleManageController : Controller
{
    private readonly IIdentityRoleRepository _identityRole;

    public RoleManageController(IIdentityRoleRepository identityRole)
    {
        _identityRole = identityRole;
    }

    [HttpGet]
    public IActionResult Role()
    {
        NewRoleViewModel model = new ();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Role(NewRoleViewModel roleViewModel)
    {
        var createdRole = await _identityRole.CreateRoleAsync(roleViewModel);
        
        if (!createdRole.Succeeded)
        {
            foreach (var item in createdRole.Errors)
            {
                ModelState.AddModelError("Role Error", item.Description);
            }
            return View("Role",roleViewModel);
        }
        else
        {
            //go to admin panel 
            return RedirectToAction("GetAllRoles");
        }

    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _identityRole.GetallRolesAsync(); 
        return View(roles);
    }

    [HttpPost]
    public async Task<IActionResult> Remove( string Name)
    {
        var x = await _identityRole.RemoveRoleAsync(Name);
        if (!x.Succeeded)
        {
            foreach (var item in x.Errors)
            {
                ModelState.AddModelError("Role Removing Error", item.Description);
                
            }    
        }
        
            return RedirectToAction("GetAllRoles");
        
    }
}