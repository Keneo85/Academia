using Academia.Business.Constants;
using Academia.DataAccess;
using Academia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Academia.API.Data
{
    // Crea datos minimos al arrancar si la base esta vacia:
    // usuario admin / Admin123* y 3 especialidades.
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AcademiaDbContext>();

            if (!await context.Set<User>().AnyAsync(u => u.Rol == Roles.Admin))
            {
                context.Set<User>().Add(new User
                {
                    UserName = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123*"),
                    Rol = Roles.Admin
                });
            }

            if (!await context.Set<Especialidad>().AnyAsync())
            {
                context.Set<Especialidad>().AddRange(
                    new Especialidad { Nombre = "Programación" },
                    new Especialidad { Nombre = "Diseño" },
                    new Especialidad { Nombre = "Idiomas" });
            }

            await context.SaveChangesAsync();
        }
    }
}
