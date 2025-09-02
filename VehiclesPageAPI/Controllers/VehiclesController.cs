using Microsoft.AspNetCore.Mvc;
using VehiclesPageAPI.Services;

namespace VehiclesPageAPI.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext context;

        public VehiclesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var vehicles = context.Vehicles.ToList();
            return View(vehicles);
        }
    }
}
