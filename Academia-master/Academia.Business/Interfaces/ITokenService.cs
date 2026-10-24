using Academia.DataAccess.Entities;

namespace Academia.Business.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}