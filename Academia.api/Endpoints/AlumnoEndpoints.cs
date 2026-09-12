using Academia.Business.Constants;
using Academia.Business.DTO.Request.Alumno;
using Academia.Business.Interfaces;
using Academia.Common.Helpers;

namespace Academia.API.Endpoints
{
    public static class AlumnoEndpoints
    {
        public static RouteGroupBuilder MapAlumnoEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateAlumnoRequest request, IAlumnoService service) =>
            {
                var result = await service.RegisterAsync(request);
                if (result.IsFailure)
                    return Results.BadRequest(result);
                return Results.Created("/api/alumnos", result);
            })
            .WithName("RegisterAlumno")
            .WithSummary("Registrar un alumno nuevo y su usuario")
            .RequireAuthorization(p => p.RequireRole(Roles.Admin))
            .Produces<Result>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            return group;
        }
    }
}