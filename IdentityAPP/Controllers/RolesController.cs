using IdentityAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPP.Controllers
{
    
    [Authorize(Roles ="admin")]
    public class RolesController:Controller
    {
        private readonly RoleManager<AppRole> _roleManager ; 
          private readonly UserManager<AppUser> _userManager ; 
        public RolesController(RoleManager<AppRole> roleManager , UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        
          public IActionResult Index()
        {
            if(!User.IsInRole("admin"))
            {
                return RedirectToAction("Index","Home");
            }
            return View(_roleManager.Roles);
        }
        public IActionResult Create()
        {
            if(!User.IsInRole("admin"))
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AppRole model)

        {
            if(!User.IsInRole("admin"))
            {
                return RedirectToAction("Index","Home");
            }
            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(model);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("",err.Description);
                }
            }
            return View();
        }

        public async Task<IActionResult> Edit (string id)
        {
            if(!User.IsInRole("admin"))
            {
                return RedirectToAction("Index","Home");
            }
            var role = await _roleManager.FindByIdAsync(id);

            if(role!=null && role.Name != null)
            {
                ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
                return View(role);
            }
            return RedirectToAction("Index");
        }

       [HttpPost]
public async Task<IActionResult> Edit(AppRole model)
{
    if(!User.IsInRole("admin"))
            {
                return RedirectToAction("Index","Home");
            }
    if (ModelState.IsValid)
    {
        var role = await _roleManager.FindByIdAsync(model.Id);
        if (role != null)
        {
            role.Name = model.Name;

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }

            // Burada role.Name kullanmak için null kontrolü yapmak gerekir
            if (role.Name != null)
            {
                ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
            }
        }
    }
    return View(model);
}
    }   
}