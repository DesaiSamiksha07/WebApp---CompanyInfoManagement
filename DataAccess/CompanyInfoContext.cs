using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CompanyInfoContext : DbContext
    {
        public CompanyInfoContext(DbContextOptions<CompanyInfoContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity properties, relationships, etc.
        }
    }
    
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    } 

}
    
