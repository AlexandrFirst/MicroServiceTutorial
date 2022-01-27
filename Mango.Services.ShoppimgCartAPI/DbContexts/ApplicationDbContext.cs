using Mango.Services.ShoppimgCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppimgCartAPI.DbContexts
{
    public class ApplicationDbContext: DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
