using FitABit.Core.Constants;
using FitABit.Core.Contracts;
using FitABit.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FItABit.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private IUserService userService;
        private IInstructorService instructorService;
        public UserController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IInstructorService instructorService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userService = userService;
            this.instructorService = instructorService;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = await instructorService.GetInstructors();
            return View(instructors);
        }
    }
}
