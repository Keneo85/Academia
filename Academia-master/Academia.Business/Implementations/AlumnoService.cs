using Academia.Business.Constants;
using Academia.Business.DTO.Request.Alumno;
using Academia.Business.DTO.Response.Alumno;
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

        public async Task<Result<List<ListAlumnosResponse>>> ListAsync(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var result = await _alumnoRepository.ListAsync(
                predicate: a => a.Status && (string.IsNullOrEmpty(search)
                    || a.Nombres.Contains(search)
                    || a.Apellidos.Contains(search)
                    || a.DNI.Contains(search)),
                selector: a => new ListAlumnosResponse
                {
                    Id = a.Id,
                    Nombres = a.Nombres,
                    Apellidos = a.Apellidos,
                    Email = a.Email,
                    Telefono = a.Telefono,
                    DNI = a.DNI,
                    UserName = a.User!.UserName,
                    CreatedAt = a.CreatedAt
                },
                pageNumber: pageNumber,
                pageSize: pageSize
            );
            return Result.Success(result.Result.ToList());
        }
    }
}