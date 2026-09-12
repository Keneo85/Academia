using Academia.Business.DTO.Request.Auth;
using Academia.Business.DTO.Response.Auth;
using Academia.Common.Helpers;

namespace Academia.Business.Interfaces
{
    public interface IAuthService
    {
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request);
    }
}