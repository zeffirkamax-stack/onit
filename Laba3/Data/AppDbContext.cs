using Microsoft.EntityFrameworkCore;
using Laba3.Models;

namespace Laba3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Laba3.Models.Employee> Employee { get; set; } = default!;
        public DbSet<Laba3.Models.Profession> Profession { get; set; } = default!;
        public DbSet<Laba3.Models.Emp_Prof> Emp_Prof { get; set; } = default!;
        public DbSet<Laba3.Models.PhoneNumber> PhoneNumber { get; set; } = default!;

    }
}
