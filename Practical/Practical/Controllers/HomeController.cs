
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

        public IActionResult Index(string carName, int pageIndex = 1, int pageSize = 6)
        {
            var carsQuery = db.Cars.Include(c => c.Version).ThenInclude(v => v.Model).AsQueryable();

            // Apply filter if carName is provided
            if (!string.IsNullOrEmpty(carName))
            {
                carsQuery = carsQuery.Where(c => c.Brand.Contains(carName));
                ViewBag.CarName = carName; // Store the filter value for the view
            }

            var count = carsQuery.Count();
            var items = carsQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            // Set pagination details in ViewBag
            ViewBag.CurrentPage = pageIndex;
            ViewBag.TotalPages = (int)Math.Ceiling((double)count / pageSize);

            return View(items);
        }






        public IActionResult CarDetails()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    
        public IActionResult Search(string query)
        {

            var results = db.Cars.Where(c => c.Brand.Contains(query)).ToList();
            return View(results);
        }

    }
}