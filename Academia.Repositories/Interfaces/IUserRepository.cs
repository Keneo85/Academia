using Academia.DataAccess.Entities;

namespace Academia.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUserNameAsync(string userName);
    }
}