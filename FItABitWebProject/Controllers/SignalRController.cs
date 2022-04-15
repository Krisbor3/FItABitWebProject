using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FItABit.Controllers
{
    public class SignalRController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
