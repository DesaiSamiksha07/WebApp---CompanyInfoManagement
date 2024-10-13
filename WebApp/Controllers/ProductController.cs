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
public class ProductController : ControllerBase
{
    private readonly CompanyInfoContext _context;

    public ProductController(CompanyInfoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.FromSqlRaw("EXEC dbo.GetAllProducts").ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FromSqlRaw("EXEC dbo.GetProductById @productId", new SqlParameter("@productId", id)).FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        var parameters = new[]
        {
            new SqlParameter("@ProductName", product.ProductName),
            new SqlParameter("@Description", product.Description)
        };

        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.InsertProduct @ProductName, @Description", parameters);

        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest();
        }

        var parameters = new[]
        {
            new SqlParameter("@ProductId", product.ProductId),
            new SqlParameter("@ProductName", product.ProductName),
            new SqlParameter("@Description", product.Description)
        };

        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateProduct @ProductId, @ProductName, @Description", parameters);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.DeleteProduct @ProductId", new SqlParameter("@ProductId", id));

        return NoContent();
    }
}
    
