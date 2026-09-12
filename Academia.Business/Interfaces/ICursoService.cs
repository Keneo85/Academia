using Academia.Business.DTO.Request.Curso;
using Academia.Business.DTO.Response.Curso;
using Academia.Common.Helpers;

namespace Academia.Business.Interfaces
{
    public interface ICursoService
    {
        Task<Result> CreateAsync(AddCursoRequest request);
        Task<Result<GetCursoResponse>> GetByIdAsync(int id);
        Task<Result> UpdateAsync(int id, UpdateCursoRequest request);
        Task<Result> DeleteAsync(int id);
        Task<Result<List<ListCursosResponse>>> ListAsync(int pageNumber = 1, int pageSize = 10);
    }
}