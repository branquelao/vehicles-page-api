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

        public IActionResult Edit(int id)
        {
            var vehicle = context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return RedirectToAction("Index", "Vehicles");
            }

            var vehicleDto = new VehicleDto()
            {
                Make = vehicle.Make,
                Model = vehicle.Model,
                Category = vehicle.Category,
                Price = vehicle.Price,
                Description = vehicle.Description,
            };

            ViewData["VehicleId"] = vehicle.Id;
            ViewData["ImageFileName"] = vehicle.ImageFileName;
            ViewData["CreatedAt"] = vehicle.CreatedAt.ToString("dd/MM/yyyy");

            return View(vehicleDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, VehicleDto vehicleDto)
        {
            var vehicle = context.Vehicles.Find(id);

            if(vehicle == null)
            {
                return RedirectToAction("Index", "Vehicles");
            }

            string newFileName = vehicle.ImageFileName;
            if(vehicleDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(vehicleDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/vehicles/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    vehicleDto.ImageFile.CopyTo(stream);
                }

                string oldImageFullPath = environment.WebRootPath + "/vehicles/" + vehicle.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            vehicle.Make = vehicleDto.Make;
            vehicle.Model = vehicleDto.Model;
            vehicle.Category = vehicleDto.Category;
            vehicle.Price = vehicleDto.Price;
            vehicle.Description = vehicleDto.Description;
            vehicle.ImageFileName = newFileName;

            context.SaveChanges();
            return RedirectToAction("Index", "Vehicles");
        }

        public IActionResult Delete(int id)
        {
            var vehicle = context.Vehicles.Find(id);
            if (vehicle == null)
            {
                return RedirectToAction("Index", "Vehicles");
            }
            string imageFullPath = environment.WebRootPath + "/vehicles/" + vehicle.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Vehicles.Remove(vehicle);
            context.SaveChanges(true);

            return RedirectToAction("Index", "Vehicles");
        }
    }
}
