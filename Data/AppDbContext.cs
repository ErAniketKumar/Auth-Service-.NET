using _NET_Practice_1.Entities;
using Microsoft.EntityFrameworkCore;

namespace _NET_Practice_1.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> AccountUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }


    }
}
