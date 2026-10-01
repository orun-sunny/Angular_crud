using Microsoft.EntityFrameworkCore;

namespace Asp.netcore_with_angular.Model
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}