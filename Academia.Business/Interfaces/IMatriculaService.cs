using Academia.Business.DTO.Request.Matricula;
using Academia.Common.Helpers;

namespace Academia.Business.Interfaces
{
    public interface IMatriculaService
    {
        Task<Result> CreateAsync(CreateMatriculaRequest request);
    }
}