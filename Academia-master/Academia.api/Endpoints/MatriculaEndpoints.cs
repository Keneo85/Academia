using Academia.Business.Constants;
using Academia.Business.DTO.Request.Matricula;
using Academia.Business.DTO.Response.Matricula;
using Academia.Business.Interfaces;
using Academia.Common.Helpers;
using System.Security.Claims;

namespace Academia.API.Endpoints
{
    public static class MatriculaEndpoints
    {
        public static RouteGroupBuilder MapMatriculaEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateMatriculaRequest request, IMatriculaService service) =>
            {
                var result = await service.CreateAsync(request);
                if (result.IsFailure)
                    return Results.BadRequest(result);
                return Results.Created("/api/matriculas", result);
            })
            .WithName("CreateMatricula")
            .WithSummary("Registrar una matricula")
            .RequireAuthorization(p => p.RequireRole(Roles.Admin))
            .Produces<Result>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            group.MapGet("/", async (IMatriculaService service, int pageNumber = 1, int pageSize = 10, int? alumnoId = null) =>
            {
                var result = await service.ListAsync(pageNumber, pageSize, alumnoId);
                return Results.Ok(result);
            })
            .WithName("ListMatriculas")
            .WithSummary("Listado de matriculas (opcional: filtrar por alumnoId)")
            .RequireAuthorization(p => p.RequireRole(Roles.Admin))
            .Produces<List<ListMatriculasResponse>>(StatusCodes.Status200OK);

            // "/mias" va ANTES de "/{id:int}" solo por orden de lectura; el :int ya evita conflictos
            group.MapGet("/mias", async (ClaimsPrincipal user, IMatriculaService service) =>
            {
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var result = await service.ListByUserAsync(userId);
                if (result.IsFailure)
                    return Results.BadRequest(result);
                return Results.Ok(result);
            })
            .WithName("MisMatriculas")
            .WithSummary("Matriculas del alumno que inicio sesion")
            .RequireAuthorization(p => p.RequireRole(Roles.Alumno))
            .Produces<List<ListMatriculasResponse>>(StatusCodes.Status200OK);

            group.MapGet("/{id:int}", async (int id, IMatriculaService service) =>
            {
                var result = await service.GetByIdAsync(id);
                if (result.IsFailure)
                    return Results.NotFound(result);
                return Results.Ok(result);
            })
            .WithName("GetMatriculaById")
            .WithSummary("Detalle de una matricula con sus cursos")
            .RequireAuthorization()
            .Produces<GetMatriculaResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return group;
        }
    }
}