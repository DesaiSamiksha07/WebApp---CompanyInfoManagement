
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers;
public class UserController : Controller
{
    private readonly CompanyInfoContext _context;

    public UserController(CompanyInfoContext context)
    {
        _context = context;
    }

    /*
    public async Task<IActionResult> Index()
    {
        return View(await _context.Users.FromSqlRaw("EXEC dbo.GetAllUsers").ToListAsync());
    }
   */

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.FromSqlRaw("EXEC dbo.GetAllUsers").ToListAsync();
        return View(users);
    }

    public async Task<IActionResult> Details(int id)
    {
        var users = await _context.Users.FromSqlRaw("EXEC dbo.GetUserById @userId", new SqlParameter("@userId", id)).ToListAsync();
        var user = users.FirstOrDefault();

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserName,Address")] User user)
    {
        if (ModelState.IsValid)
        {
            var parameters = new[]
            {
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@Address", user.Address)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC dbo.InsertUser @UserName, @Address", parameters);

            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var users = await _context.Users.FromSqlRaw("EXEC dbo.GetUserById @userId", new SqlParameter("@userId", id)).ToListAsync();
        var user = users.FirstOrDefault();
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Address")] User user)
    {
        if (id != user.UserId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var parameters = new[]
            {
                new SqlParameter("@userId", user.UserId),
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@Address", user.Address)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateUser @userId, @UserName, @Address", parameters);

            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var users = await _context.Users.FromSqlRaw("EXEC dbo.GetUserById @userId", new SqlParameter("@userId", id)).ToListAsync();
        var user = users.FirstOrDefault();
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC dbo.DeleteUser @userId", new SqlParameter("@userId", id));
        return RedirectToAction(nameof(Index));
    }
}
