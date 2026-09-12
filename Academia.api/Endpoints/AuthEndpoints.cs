using Academia.Business.DTO.Request.Auth;
using Academia.Business.DTO.Response.Auth;
using Academia.Business.Interfaces;

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

            return group;
        }
    }
}