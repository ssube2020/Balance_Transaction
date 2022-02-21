using Microsoft.EntityFrameworkCore;

namespace Balance_Transaction.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
