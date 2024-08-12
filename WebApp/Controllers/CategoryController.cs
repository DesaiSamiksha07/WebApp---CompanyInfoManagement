using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CompanyInfoContext _context;

    public CategoryController(CompanyInfoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _context.Categories.FromSqlRaw("EXEC dbo.GetAllCategories").ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Categories.FromSqlRaw("EXEC dbo.GetCategoryById @categoryId", new SqlParameter("@categoryId", id)).FirstOrDefaultAsync();

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        var parameters = new[]
        {
            new SqlParameter("@CategoryName", category.CategoryName)
        };

        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.InsertCategory @CategoryName", parameters);

        return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        var parameters = new[]
        {
            new SqlParameter("@CategoryId", category.CategoryId),
            new SqlParameter("@CategoryName", category.CategoryName)
        };

        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateCategory @CategoryId, @CategoryName", parameters);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.DeleteCategory @CategoryId", new SqlParameter("@CategoryId", id));

        return NoContent();
    }
}
