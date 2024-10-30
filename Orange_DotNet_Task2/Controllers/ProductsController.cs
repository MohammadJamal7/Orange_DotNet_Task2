using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orange_DotNet_Task2.Data;
using Orange_DotNet_Task2.Models;
using System.Runtime.InteropServices;

namespace Orange_DotNet_Task2.Controllers
{
    public class ProductsController : Controller
    {
        
        private  ApplicationContext _context;


        public ProductsController(ApplicationContext context)
        {

           _context = context;
        }
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Products.Include(p=>p.category).ToListAsync());
        }


        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                product.imagePath = "/images/" + fileName;
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
