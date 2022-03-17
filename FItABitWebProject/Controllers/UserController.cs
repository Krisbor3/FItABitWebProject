using Microsoft.AspNetCore.Mvc;

namespace FItABit.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
