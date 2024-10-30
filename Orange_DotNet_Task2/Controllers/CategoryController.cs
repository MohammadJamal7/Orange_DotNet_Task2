using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Orange_DotNet_Task2.Data;
using Orange_DotNet_Task2.Models;

namespace Orange_DotNet_Task2.Controllers
{
	public class CategoryController : Controller
	{
		public CategoryController(ApplicationContext context)
		{
			_context = context;
		}
		private readonly ApplicationContext _context;

		public async Task<IActionResult> Index()
		{

			return View(await _context.Categories.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category cat, IFormFile imageFile)
		{
			
				if (imageFile != null)
				{
					var fileName = Path.GetFileName(imageFile.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await imageFile.CopyToAsync(stream);
					}

					cat.imagePath = "/images/" + fileName;
				}

				_context.Add(cat);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			
			
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}