using FitABit.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FItABit.Controllers
{
    [Authorize]
    public class ProgramController : Controller
    {
        private readonly IProgramService programService;

        public ProgramController(IProgramService programService)
        {
            this.programService = programService;
        }
        public IActionResult ProgramLayout()
        {
            return View();
        }
        public async Task<IActionResult> GetAllPrograms()
        {
            var programs = await programService.GetPrograms();

            return View(programs);
        }

        public async Task<IActionResult> See(string id)
        {
            var exercises = await programService.GetPrograms();
            return View(exercises);
        }
    }
}
