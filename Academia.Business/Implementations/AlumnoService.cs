using Academia.Business.Constants;
using Academia.Business.DTO.Request.Alumno;
using Academia.Business.Interfaces;
using Academia.Common.Helpers;
using Academia.DataAccess.Entities;
using Academia.Repositories.Interfaces;
using Mapster;

namespace Academia.Business.Implementations
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        public AlumnoService(IAlumnoRepository alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }

        public async Task<Result> RegisterAsync(CreateAlumnoRequest request)
        {
            var userNameExists = await _alumnoRepository.UserExistsAsync(request.UserName);
            if (userNameExists)
                return Result.Failure($"El nombre de usuario {request.UserName} ya existe.");

            var emailExists = await _alumnoRepository.GetByPredicateAsync(a => a.Email == request.Email);
            if (emailExists != null)
                return Result.Failure($"El correo {request.Email} ya está siendo usado.");

            var dniExists = await _alumnoRepository.GetByPredicateAsync(a => a.DNI == request.DNI);
            if (dniExists != null)
                return Result.Failure($"El DNI {request.DNI} ya está registrado.");

            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Rol = Roles.Alumno
            };

            var alumno = request.Adapt<Alumno>();

            await _alumnoRepository.CreateWithUserAsync(alumno, user);

            return Result.Success("Alumno registrado exitosamente.");
        }
    }
}