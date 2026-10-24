using Academia.Business.DTO.Response.Especialidad;
using Academia.Common.Helpers;

namespace Academia.Business.Interfaces
{
    public interface IEspecialidadService
    {
        Task<Result<List<ListEspecialidadesResponse>>> ListAsync();
    }
}
