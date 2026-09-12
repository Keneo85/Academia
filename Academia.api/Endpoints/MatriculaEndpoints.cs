using Academia.Business.Constants;
using Academia.Business.DTO.Request.Matricula;
using Academia.Business.Interfaces;
using Academia.Common.Helpers;

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

            return group;
        }
    }
}