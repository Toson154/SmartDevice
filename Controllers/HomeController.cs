using Devices_E_Commerce.Data;
using Devices_E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Devices_E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<IActionResult> Privacy(int? categoryId, string searchName)
        {
            IQueryable<Device>
                devices = _context.Devices.Include(d => d.Category);

            if (categoryId.HasValue)
            {
                devices = devices.Where(d => d.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                devices = devices.Where(d => d.Name.Contains(searchName));
            }
            var model = await devices.ToListAsync();

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SearchName = searchName;

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}