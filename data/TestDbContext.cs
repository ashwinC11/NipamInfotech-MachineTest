using Microsoft.EntityFrameworkCore;
using NipamInfotech_MachineTest.Models;

namespace NipamInfotech_MachineTest.data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }



        public DbSet<Product> products { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}
