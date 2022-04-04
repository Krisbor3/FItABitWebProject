using FitABit.Core.Contracts;
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
    }
}
