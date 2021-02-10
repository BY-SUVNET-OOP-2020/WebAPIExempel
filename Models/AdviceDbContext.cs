using Microsoft.EntityFrameworkCore;

namespace WebApiTest.Models
{
    public class AdviceDbContext : DbContext
    {
        public AdviceDbContext(DbContextOptions<AdviceDbContext> options): base(options) { }

        public DbSet<Advice> Advices { get; set; }
    }
}