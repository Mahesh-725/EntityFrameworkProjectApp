using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProjectApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
