using Academia.Business.DTO.Response.Especialidad;
using Academia.Business.Interfaces;

namespace Academia.API.Endpoints
{
    public static class EspecialidadEndpoints
    {
        public static RouteGroupBuilder MapEspecialidadEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IEspecialidadService service) =>
            {
                var result = await service.ListAsync();
                return Results.Ok(result);
            })
            .WithName("ListEspecialidades")
            .WithSummary("Listado de especialidades")
            .RequireAuthorization()
            .Produces<List<ListEspecialidadesResponse>>(StatusCodes.Status200OK);

            return group;
        }
    }
}
