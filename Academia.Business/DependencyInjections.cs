using Academia.Business.Implementations;
using Academia.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Academia.Business
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IAlumnoService, AlumnoService>();
            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<IMatriculaService, MatriculaService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}