
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CompanyInfoContext _context;

        public CategoryController(CompanyInfoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.FromSqlRaw("EXEC dbo.GetAllCategories").ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var categories = await _context.Categories.FromSqlRaw("EXEC dbo.GetCategoryById @categoryId", new SqlParameter("@categoryId", id)).ToListAsync();
            var category = categories.FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                var parameters = new[]
                {
                    new SqlParameter("@CategoryName", category.CategoryName)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.InsertCategory @CategoryName", parameters);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _context.Categories.FromSqlRaw("EXEC dbo.GetCategoryById @categoryId", new SqlParameter("@categoryId", id)).ToListAsync();
            var category = categories.FirstOrDefault();


            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var parameters = new[]
                {
                    new SqlParameter("@CategoryId", category.CategoryId),
                    new SqlParameter("@CategoryName", category.CategoryName)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateCategory @CategoryId, @CategoryName", parameters);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categories = await _context.Categories.FromSqlRaw("EXEC dbo.GetCategoryById @categoryId", new SqlParameter("@categoryId", id)).ToListAsync();
            var category = categories.FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC dbo.DeleteCategory @CategoryId", new SqlParameter("@CategoryId", id));
            return RedirectToAction(nameof(Index));
        }
    }
}
