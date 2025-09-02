using Microsoft.AspNetCore.Mvc;

namespace VehiclesPageAPI.Controllers
{
    public class VehiclesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
