using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Academia.DataAccess
{
    public class AcademiaDbContextFactory : IDesignTimeDbContextFactory<AcademiaDbContext>
    {
        public AcademiaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AcademiaDbContext>();
            optionsBuilder.UseSqlServer("server=localhost, 1502; database=AcademiaDb; uid=sa; password=Academia@2026; encrypt=False;");
            return new AcademiaDbContext(optionsBuilder.Options);
        }
    }
}