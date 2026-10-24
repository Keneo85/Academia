using Academia.Business.DTO.Request.Matricula;
using Academia.Business.DTO.Response.Matricula;
using Academia.Common.Helpers;

namespace Academia.Business.Interfaces
{
    public interface IMatriculaService
    {
        Task<Result> CreateAsync(CreateMatriculaRequest request);
        Task<Result<List<ListMatriculasResponse>>> ListAsync(int pageNumber = 1, int pageSize = 10, int? alumnoId = null);
        Task<Result<List<ListMatriculasResponse>>> ListByUserAsync(int userId);
        Task<Result<GetMatriculaResponse>> GetByIdAsync(int id);
    }
}
