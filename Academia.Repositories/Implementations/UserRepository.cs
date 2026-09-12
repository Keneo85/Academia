using Academia.DataAccess;
using Academia.DataAccess.Entities;
using Academia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Academia.Repositories.Implementations
{
    public class UserRepository(AcademiaDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetByUserNameAsync(string userName)
            => await _context.Set<User>().FirstOrDefaultAsync(u => u.UserName == userName && u.Status);
    }
}