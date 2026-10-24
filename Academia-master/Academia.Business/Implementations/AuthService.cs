using Academia.Business.DTO.Request.Auth;
using Academia.Business.DTO.Response.Auth;
using Academia.Business.Interfaces;
using Academia.Common.Helpers;
using Academia.Repositories.Interfaces;

namespace Academia.Business.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUserNameAsync(request.UserName);
            if (user is null)
                return Result.Failure<LoginResponse>("Usuario o contraseña incorrectos.");

            var isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isValid)
                return Result.Failure<LoginResponse>("Usuario o contraseña incorrectos.");

            var token = _tokenService.GenerateToken(user);
            return Result.Success(new LoginResponse
            {
                Token = token,
                ExpirationDate = DateTime.UtcNow.AddHours(8),
                Role = user.Rol
            });
        }
    }
}