using Academia.Business.DTO.Request.Alumno;
using Academia.Business.DTO.Request.Auth;
using Academia.Business.DTO.Response.Auth;
using Academia.Business.Interfaces;
using Academia.Common.Helpers;

namespace Academia.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/login", async (LoginRequest request, IAuthService authService) =>
            {
                var result = await authService.LoginAsync(request);
                if (result.IsFailure)
                    return Results.BadRequest(result);
                return Results.Ok(result);
            })
            .WithName("Login")
            .WithSummary("Iniciar sesion")
            .AllowAnonymous()
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            // Registro publico: crea el alumno y su usuario con rol Alumno.
            // Reutiliza AlumnoService.RegisterAsync (mismas validaciones de usuario, email y DNI).
            group.MapPost("/register", async (CreateAlumnoRequest request, IAlumnoService service) =>
            {
                var result = await service.RegisterAsync(request);
                if (result.IsFailure)
                    return Results.BadRequest(result);
                return Results.Created("/api/auth/login", result);
            })
            .WithName("Register")
            .WithSummary("Registrarse como alumno (crea alumno + usuario)")
            .AllowAnonymous()
            .Produces<Result>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            return group;
        }
    }
}
