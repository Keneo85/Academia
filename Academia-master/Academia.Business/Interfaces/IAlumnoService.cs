using Academia.Business.DTO.Request.Alumno;
using Academia.Business.DTO.Response.Alumno;
using Academia.Common.Helpers;

namespace Academia.Business.Interfaces
{
    public interface IAlumnoService
    {
        Task<Result> RegisterAsync(CreateAlumnoRequest request);
        Task<Result<List<ListAlumnosResponse>>> ListAsync(int pageNumber = 1, int pageSize = 10, string? search = null);
    }
}
