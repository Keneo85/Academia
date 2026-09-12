using Academia.Business.DTO.Request.Alumno;
using Academia.Common.Helpers;

namespace Academia.Business.Interfaces
{
    public interface IAlumnoService
    {
        Task<Result> RegisterAsync(CreateAlumnoRequest request);
    }
}