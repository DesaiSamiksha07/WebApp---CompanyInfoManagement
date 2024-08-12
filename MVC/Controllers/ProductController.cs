
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly CompanyInfoContext _context;

        public ProductController(CompanyInfoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.FromSqlRaw("EXEC dbo.GetAllProducts").ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var products = await _context.Products.FromSqlRaw("EXEC dbo.GetProductById @productId", new SqlParameter("@productId", id)).ToListAsync();
            var product = products.FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                var parameters = new[]
                {
                    new SqlParameter("@ProductName", product.ProductName),
                    new SqlParameter("@Description", product.Description)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.InsertProduct @ProductName, @Description", parameters);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var products = await _context.Products.FromSqlRaw("EXEC dbo.GetProductById @productId", new SqlParameter("@productId", id)).ToListAsync();
            var product = products.FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description")] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var parameters = new[]
                {
                    new SqlParameter("@ProductId", product.ProductId),
                    new SqlParameter("@ProductName", product.ProductName),
                    new SqlParameter("@Description", product.Description)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateProduct @ProductId, @ProductName, @Description", parameters);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var products = await _context.Products.FromSqlRaw("EXEC dbo.GetProductById @productId", new SqlParameter("@productId", id)).ToListAsync();
            var product = products.FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC dbo.DeleteProduct @ProductId", new SqlParameter("@ProductId", id));
            return RedirectToAction(nameof(Index));
        }
    }
}
