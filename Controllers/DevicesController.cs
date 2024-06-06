using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Devices_E_Commerce.Data;
using Devices_E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting; // Add this line

namespace Devices_E_Commerce.Controllers
{
    public class DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Devices.Include(d => d.Category);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult temp()
        {
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Categories()
        {
            var categories = _context.Categories.ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(categories);
        }

        public IActionResult Products(int categoryId)
        {
            // Retrieve devices based on the categoryId
            var devicesInCategory = _context.Devices
                .Where(d => d.CategoryId == categoryId)
                .ToList();

            return View(devicesInCategory);
        }

        public IActionResult ProductDetails(int productId)
        {
            // Fetch the product details based on the productId from your data source
            var product = _context.Devices.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return NotFound(); // Or handle the case when the product is not found
            }

            // Fetch other related products based on the product category
            var relatedProducts = _context.Devices.Where(p => p.CategoryId == product.CategoryId && p.Id != productId).ToList();

            // You can pass the product and related products to the view
            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts
            };

            return View(viewModel);
        }


        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Devices == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        public IActionResult Create()
        {
            List<Category> categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device, IFormFile imageFile)
        {
        
                // Handle image upload if provided
                if (imageFile != null && imageFile.Length > 0)
                {

                    string filename = imageFile.FileName;
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    { await imageFile.CopyToAsync(filestream); };
                    device.DeviceImage = filename;
                _context.Add(device);
                await _context.SaveChangesAsync();
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", device.CategoryId);
                return RedirectToAction(nameof(Index));
               
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", device.CategoryId);
            return View(device);
        }


        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Devices == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", device.CategoryId);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", device.CategoryId);
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Devices == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Devices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Devices'  is null.");
            }
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
          return (_context.Devices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
