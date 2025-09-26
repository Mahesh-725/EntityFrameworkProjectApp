using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProjectApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Title = "USD",Description="Indian INR" },
                new Currency { Id = 2, Title = "EUR", Description = "Euro" },
                new Currency { Id = 3, Title = "Dollar", Description = "Dollar" },
                new Currency { Id = 4, Title = "Dinar", Description = "Dinar" }
            );

            modelBuilder.Entity<Language>().HasData(
               new Currency { Id = 1, Title = "Hindi", Description = "Hindi" },
               new Currency { Id = 2, Title = "Tamil", Description = "Tamil" },
               new Currency { Id = 3, Title = "Punjabi", Description = "Punjabi" },
               new Currency { Id = 4, Title = "Urdu", Description = "Urdu" }
           );
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
