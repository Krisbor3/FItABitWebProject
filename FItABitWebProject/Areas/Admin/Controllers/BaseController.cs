using FitABit.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FItABit.Areas.Admin.Controllers
{
    [Authorize(Roles =UserConstants.Roles.Administrator)]
        [Area("Admin")]
    public class BaseController : Controller
    {
    }
}
