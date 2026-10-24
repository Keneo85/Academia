using Academia.DataAccess.Entities;

namespace Academia.Repositories.Interfaces
{
    public interface IMatriculaRepository : IBaseRepository<Matricula>
    {
        Task<Matricula?> GetWithDetallesAsync(int id);
    }
}
