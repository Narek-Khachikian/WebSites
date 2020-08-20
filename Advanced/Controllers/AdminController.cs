using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Advanced.Models.ViewModels;
using Advanced.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace Advanced.Controllers
{
    [Authorize(Roles ="Admins")]
    public class AdminController : Controller
    {
        public UserManager<IdentityUser> UserManager;

        public AdminController(UserManager<IdentityUser> manager)
        {
            UserManager = manager;
        }

        public IActionResult UserList()
        {
            AdminViewModel model = new AdminViewModel()
            {
                Users = UserManager.Users
            };
            return View("List", model);
        }

        public IActionResult Create()
        {
            return View("CreateUser");
        }

        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create([FromForm] UserEditingViewModel CreateModel)
        {
            if (ModelState.IsValid && CreateModel.Password!=null)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = CreateModel.UserName,
                    Email = CreateModel.Email
                };
                IdentityResult result = await UserManager.CreateAsync(user, CreateModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UserList));
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            return View("CreateUser");
        }


        public async Task<IActionResult> Editor(string id, string mode)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);
            UserEditingViewModel model = new UserEditingViewModel()
            {
                Email = user.Email,
                UserName = user.UserName,
                Id = id,
                Password = "TestPass123$"
            };
            return View("Editor", model);
        }


        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Editor([FromForm] UserEditingViewModel EditModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await UserManager.FindByIdAsync(EditModel.Id);
                user.UserName = EditModel.UserName;
                user.Email = EditModel.Email;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if(result.Succeeded && !String.IsNullOrEmpty(EditModel.Password) && EditModel.Password != "TestPass123$")
                {
                    await UserManager.RemovePasswordAsync(user);
                    result = await UserManager.AddPasswordAsync(user, EditModel.Password);
                }
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UserList));
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(EditModel);
        }


        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ListDelete([FromForm] string id)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);
            IdentityResult result = null;
            if (user != null)
            {
                result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UserList));
                }
            }
            foreach(IdentityError item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return RedirectToAction(nameof(UserList));
            
        }
    }
}
