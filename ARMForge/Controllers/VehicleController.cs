using Microsoft.AspNetCore.Mvc;

namespace ARMForge.Web.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
