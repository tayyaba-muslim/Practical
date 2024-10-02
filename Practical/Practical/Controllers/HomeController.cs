using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Practical.Models;

using System.Diagnostics;

namespace Practical.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly PracticalContext db;

        public HomeController(PracticalContext context)
        {
            db = context;
        }
        public IActionResult Index(string carName, int page = 1)
        {
            var cars = db.Cars.Include(c => c.Version).ThenInclude(v => v.Model);

            // Apply filter if carName is provided
            if (!string.IsNullOrEmpty(carName))
            {
                cars = cars.Where(c => c.Brand.Contains(carName)); // Adjust this based on how you want to filter
            }

            int pageSize = 6; // Number of items per page
            var paginatedCars = cars.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)cars.Count() / pageSize);

            return View(paginatedCars);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetCars()
        {
            var data = db.Cars.Include(c => c.Version).ThenInclude(v => v.Model).ToList();

            return View(data); 

        }
        public IActionResult Search(string query)
        {
            // Implement search logic here
            // e.g., filter cars based on the query
            var results = db.Cars.Where(c => c.Brand.Contains(query)).ToList();
            return View(results);
        }

    }
}