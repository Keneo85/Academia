using Academia.Business.Constants;
using Academia.Business.DTO.Request.Alumno;
using Academia.Business.DTO.Response.Alumno;
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

            group.MapGet("/", async (IAlumnoService service, int pageNumber = 1, int pageSize = 10, string? search = null) =>
            {
                var result = await service.ListAsync(pageNumber, pageSize, search);
                return Results.Ok(result);
            })
            .WithName("ListAlumnos")
            .WithSummary("Listado de alumnos (busca por nombre, apellido o DNI)")
            .RequireAuthorization(p => p.RequireRole(Roles.Admin))
            .Produces<List<ListAlumnosResponse>>(StatusCodes.Status200OK);

            return group;
        }
    }
}