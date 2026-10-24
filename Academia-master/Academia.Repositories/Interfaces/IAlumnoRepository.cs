using Academia.Common.Helpers;
using Academia.DataAccess.Entities;

namespace Academia.Repositories.Interfaces
{
    public interface IAlumnoRepository : IBaseRepository<Alumno>
    {
        Task<Result> CreateWithUserAsync(Alumno alumno, User user);
        Task<bool> UserExistsAsync(string userName);
    }
}