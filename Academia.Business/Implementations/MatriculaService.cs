using Academia.Business.DTO.Request.Matricula;
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

        public MatriculaService(IMatriculaRepository matriculaRepository, ICursoRepository cursoRepository)
        {
            _matriculaRepository = matriculaRepository;
            _cursoRepository = cursoRepository;
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
    }
}