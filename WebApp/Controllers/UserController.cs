using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly CompanyInfoContext _context;

    public UserController(CompanyInfoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.FromSqlRaw("EXEC dbo.GetAllUsers").ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FromSqlRaw("EXEC dbo.GetUserById @userId", new SqlParameter("@userId", id)).FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        var parameters = new[]
        {
            new SqlParameter("@userId", user.UserId),
            new SqlParameter("@UserName", user.UserName),
            new SqlParameter("@Address", user.Address)
        };

        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.InsertUser @userId, @UserName, @Address", parameters);

        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.UserId)
        {
            return BadRequest();
        }

        var parameters = new[]
        {
            new SqlParameter("@userId", user.UserId),
            new SqlParameter("@UserName", user.UserName),
            new SqlParameter("@Address", user.Address)
        };

        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateUser @userId, @UserName, @Address", parameters);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.DeleteUser @userId", new SqlParameter("@userId", id));

        return NoContent();
    }
}

