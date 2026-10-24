using Academia.Business.DTO.Request.Matricula;
using Academia.Business.DTO.Response.Matricula;
using Academia.Business.Interfaces;
using Academia.Common.Helpers;
using Academia.DataAccess.Entities;
using Academia.Repositories.Interfaces;

namespace Academia.Business.Implementations
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IAlumnoRepository _alumnoRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository, ICursoRepository cursoRepository, IAlumnoRepository alumnoRepository)
        {
            _matriculaRepository = matriculaRepository;
            _cursoRepository = cursoRepository;
            _alumnoRepository = alumnoRepository;
        }

        public async Task<Result> CreateAsync(CreateMatriculaRequest request)
        {
            var detalles = new List<DetalleMatricula>();
            decimal total = 0;

            foreach (var item in request.Detalles)
            {
                var curso = await _cursoRepository.GetByIdAsync(item.CursoId);
                if (curso is null)
                    return Result.Failure($"El curso con ID {item.CursoId} no existe.");

                var detalle = new DetalleMatricula
                {
                    CursoId = item.CursoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = curso.Precio
                };

                total += detalle.SubTotal;
                detalles.Add(detalle);
            }

            var matricula = new Matricula
            {
                AlumnoId = request.AlumnoId,
                Total = total,
                Detalles = detalles
            };

            await _matriculaRepository.CreateAsync(matricula);
            return Result.Success("Matrícula registrada exitosamente.");
        }

        public async Task<Result<List<ListMatriculasResponse>>> ListAsync(int pageNumber = 1, int pageSize = 10, int? alumnoId = null)
        {
            var result = await _matriculaRepository.ListAsync(
                predicate: m => m.Status && (alumnoId == null || m.AlumnoId == alumnoId),
                selector: m => new ListMatriculasResponse
                {
                    Id = m.Id,
                    AlumnoId = m.AlumnoId,
                    Alumno = m.Alumno!.Nombres + " " + m.Alumno.Apellidos,
                    Total = m.Total,
                    CantidadCursos = m.Detalles.Count,
                    CreatedAt = m.CreatedAt
                },
                pageNumber: pageNumber,
                pageSize: pageSize
            );
            return Result.Success(result.Result.ToList());
        }

        // Matrículas del alumno que inició sesión (userId viene del token)
        public async Task<Result<List<ListMatriculasResponse>>> ListByUserAsync(int userId)
        {
            var alumno = await _alumnoRepository.GetByPredicateAsync(a => a.UserId == userId && a.Status);
            if (alumno is null)
                return Result.Failure<List<ListMatriculasResponse>>("El usuario no está asociado a un alumno.");
            return await ListAsync(1, 100, alumno.Id);
        }

        public async Task<Result<GetMatriculaResponse>> GetByIdAsync(int id)
        {
            var m = await _matriculaRepository.GetWithDetallesAsync(id);
            if (m is null)
                return Result.Failure<GetMatriculaResponse>("Matrícula no encontrada.");

            return Result.Success(new GetMatriculaResponse
            {
                Id = m.Id,
                AlumnoId = m.AlumnoId,
                Alumno = $"{m.Alumno?.Nombres} {m.Alumno?.Apellidos}",
                Total = m.Total,
                CreatedAt = m.CreatedAt,
                Detalles = m.Detalles.Select(d => new GetDetalleMatriculaResponse
                {
                    CursoId = d.CursoId,
                    Curso = d.Curso?.Nombre ?? string.Empty,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    SubTotal = d.SubTotal
                }).ToList()
            });
        }
    }
}