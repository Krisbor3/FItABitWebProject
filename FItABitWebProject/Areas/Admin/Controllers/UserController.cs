using FitABit.Core.Constants;
using FitABit.Core.Models;
using FitABit.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FItABit.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private IUserService userService;
        public UserController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await userService.GetUsers();

            return View(users);
        }
        public async Task<IActionResult> Roles(string id)
        {
            return Ok(id);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var model = await userService.GetUsersForEdit(id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,UserEditViewModel model)
        {
            if (!ModelState.IsValid || id!= model.Id)
            {
                return View(model);
            }
            if (await userService.UpdateUser(model))
            {
                ViewData["Result"]= "Update Successfull!";
            }
            else
            {
                ViewData["Result"] = "Update Failed.";
            }

            return View(model);
        }
        public async Task<IActionResult> CreateRole()
        {
            //await roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = "Administrator"
            //});
            return Ok();
        }
    }
}

