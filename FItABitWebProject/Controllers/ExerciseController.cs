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

        public ExerciseController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
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

        public async Task<IActionResult> Details(string id)
        {
            var model = new DetailViewModel()
            {
                ExerciseId = id,
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

        public async Task<IActionResult> SeeResults(string Id)
        {
            var results = await exerciseService.SeeResults(Id);
            return View(results);
        }
    }
}
