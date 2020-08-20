using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advanced.Models.ViewModels;

namespace Advanced.Controllers
{
    public class RolesController : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public UserManager<IdentityUser> UserManager { get; set; }

        public RolesController(RoleManager<IdentityRole> roleM, UserManager<IdentityUser> userM)
        {
            RoleManager = roleM;
            UserManager = userM;
        }

        public async Task<IActionResult> RoleList()
        {
            RolesViewModel model = new RolesViewModel()
            {
                Roles = RoleManager.Roles
            };
            foreach (IdentityRole role in model.Roles)
            {
                IList<IdentityUser> list = await UserManager.GetUsersInRoleAsync(role.Name);
                string names = String.Join(", ", list);
                model.RoleMembers.Add(role.Name,names);
            }
            return View(model);
        }

        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteRole([FromForm] string id)
        {
            IdentityResult result = await RoleManager.DeleteAsync(await RoleManager.FindByIdAsync(id));
            return RedirectToAction(nameof(RoleList));
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create([FromForm] RolesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(viewModel.Role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RoleList));
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        public async Task<IActionResult> Editor(string id)
        {
            if(id.Length > 0)
            {
                IdentityRole role = await RoleManager.FindByIdAsync(id);
                IList<IdentityUser> users = await UserManager.GetUsersInRoleAsync(role.Name);
                IList<IdentityUser> nonusers = UserManager.Users.ToList().Except(users).ToList();
                RolesViewModel model = new RolesViewModel()
                {
                    Role = role,
                    Members = users,
                    nonMembers = nonusers
                };

                return View(model);
            }
            return RedirectToAction(nameof(RoleList));
        }

        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Editor([FromForm] string roleName,[FromQuery] string userid)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await UserManager.FindByIdAsync(userid);
                bool member = await UserManager.IsInRoleAsync(user, roleName);
                IdentityResult result;
                if (member)
                {
                    result = await UserManager.RemoveFromRoleAsync(user, roleName);
                }
                else
                {
                    result = await UserManager.AddToRoleAsync(user, roleName);
                }

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RoleList));
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction(nameof(Editor),new { id = RoleManager.FindByNameAsync(roleName).Id });
        }
    }
}
