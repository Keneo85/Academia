using Microsoft.EntityFrameworkCore;

namespace Academia.DataAccess
{
    public class AcademiaDbContext(DbContextOptions<AcademiaDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcademiaDbContext).Assembly);
        }
    }
}