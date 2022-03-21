using Microsoft.AspNetCore.Mvc;

namespace FItABit.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
