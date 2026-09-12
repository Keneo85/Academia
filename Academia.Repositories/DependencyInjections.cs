using Academia.Repositories.Implementations;
using Academia.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Academia.Repositories
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAlumnoRepository, AlumnoRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IMatriculaRepository, MatriculaRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}