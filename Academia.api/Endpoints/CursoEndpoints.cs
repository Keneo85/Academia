using Academia.Business.Constants;
using Academia.Business.DTO.Request.Curso;
using Academia.Business.DTO.Response.Curso;
using Academia.Business.Interfaces;

namespace Academia.API.Endpoints
{
    public static class CursoEndpoints
    {
        public static RouteGroupBuilder MapCursoEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (AddCursoRequest request, ICursoService service) =>
            {
                var result = await service.CreateAsync(request);
                return Results.Ok(result);
            })
            .WithName("CreateCurso")
            .WithSummary("Crear curso")
            .RequireAuthorization(p => p.RequireRole(Roles.Admin))
            .Produces(StatusCodes.Status200OK);

            group.MapGet("/{id:int}", async (int id, ICursoService service) =>
            {
                var result = await service.GetByIdAsync(id);
                if (result.Value == null) return Results.NotFound(result);
                return Results.Ok(result);
            })
            .WithName("GetCursoById")
            .WithSummary("Detalle de un curso")
            .RequireAuthorization()
            .Produces<GetCursoResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", async (int pageNumber, int pageSize, ICursoService service) =>
            {
                var result = await service.ListAsync(pageNumber, pageSize);
                return Results.Ok(result);
            })
            .WithName("ListCursos")
            .WithSummary("Listado de cursos")
            .RequireAuthorization()
            .Produces<List<ListCursosResponse>>(StatusCodes.Status200OK);

            group.MapPut("/{id:int}", async (int id, UpdateCursoRequest request, ICursoService service) =>
            {
                var result = await service.UpdateAsync(id, request);
                return Results.Ok(result);
            })
            .WithName("UpdateCurso")
            .WithSummary("Actualizar curso")
            .RequireAuthorization(p => p.RequireRole(Roles.Admin))
            .Produces(StatusCodes.Status200OK);

            group.MapDelete("/{id:int}", async (int id, ICursoService service) =>
            {
                var result = await service.DeleteAsync(id);
                return Results.Ok(result);
            })
            .WithName("DeleteCurso")
            .WithSummary("Eliminar curso")
            .RequireAuthorization(p => p.RequireRole(Roles.Admin))
            .Produces(StatusCodes.Status200OK);

            return group;
        }
    }
}