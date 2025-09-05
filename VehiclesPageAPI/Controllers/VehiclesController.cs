using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using VehiclesPageAPI.Models;
using VehiclesPageAPI.Services;

namespace VehiclesPageAPI.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public VehiclesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var vehicles = context.Vehicles.OrderByDescending(p => p.Id).ToList();
            return View(vehicles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VehicleDto vehicleDto)
        {
            if(vehicleDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Please select an image file.");
            }

            if (!ModelState.IsValid)
            {
                return View(vehicleDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(vehicleDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/vehicles/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                vehicleDto.ImageFile.CopyTo(stream);
            }

            Vehicle vehicle = new Vehicle
            {
                Make = vehicleDto.Make,
                Model = vehicleDto.Model,
                Category = vehicleDto.Category,
                Price = vehicleDto.Price,
                Description = vehicleDto.Description,
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now
            };

            context.Vehicles.Add(vehicle);
            context.SaveChanges();

            return RedirectToAction("Index", "Vehicles");
        }
    }
}
