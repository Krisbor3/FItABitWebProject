using FitABit.Core.Constants;
using FitABit.Core.Contracts;
using FitABit.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FItABit.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly IExerciseService exerciseService;
        private readonly IUserService userService;

        public ExerciseController(IExerciseService exerciseService,IUserService userService)
        {
            this.exerciseService = exerciseService;
            this.userService = userService;
        }
        public async Task<IActionResult> ChestDay()
        {
            var exercises = await exerciseService.GetExercisesForChestDay();
            return View(exercises);
        }

        public async Task<IActionResult> RestDay()
        {
            return View();
        }

        public async Task<IActionResult> BackDay()
        {
            var exercises = await exerciseService.GetExercisesForBackDay();
            return View(exercises);
        }

        public async Task<IActionResult> Details(string id,string email)
        {
            var user = await userService.GetUserByEmail(email);

            var model = new DetailViewModel()
            {
                ExerciseId = id,
                UserId = user.Id.ToString()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Details(DetailViewModel model)
        {
            
            if (await exerciseService.AddDetails(model))
            {
                ViewData["Result"] = "Update Successfull!";
            }
            else
            {
                ViewData["Result"] = "Update Failed.";
            }

            return Redirect("/Exercise/BackDay");
        }

        public async Task<IActionResult> SeeResults(string Id,string email)
        {
            var user = await userService.GetUserByEmail(email);
            var results = await exerciseService.SeeResults(Id,user.Id);
            return View(results);
        }
    }
}
