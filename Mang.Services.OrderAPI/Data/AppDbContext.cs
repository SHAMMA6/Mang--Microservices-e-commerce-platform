
using Mang.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mang.Services.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<OrderHeader> OrderHeaders { get; set;}
        public DbSet<OrderDetails> OrderDetails { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
