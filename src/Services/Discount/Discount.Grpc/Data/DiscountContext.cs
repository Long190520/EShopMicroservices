using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Couppons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "Book", Description = "Book couppon", Amount = 20 },
                new Coupon { Id = 2, ProductName = "Pen", Description = "Pen couppon", Amount = 5 }
                );
        }
    }
}
