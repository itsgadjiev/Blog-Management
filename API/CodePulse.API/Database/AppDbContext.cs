using CodePulse.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) :base(dbContextOptions)
        {
            
        }
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
