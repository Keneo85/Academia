using Academia.Business.DTO.Request.Curso;
using Academia.Business.DTO.Response.Curso;
using Academia.Business.Interfaces;
using Academia.Common.Helpers;
using Academia.DataAccess.Entities;
using Academia.Repositories.Interfaces;
using Mapster;

namespace Academia.Business.Implementations
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _repository;
        public CursoService(ICursoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> CreateAsync(AddCursoRequest request)
        {
            var curso = request.Adapt<Curso>();
            await _repository.CreateAsync(curso);
            return Result.Success("Curso creado exitosamente.");
        }

        public async Task<Result<GetCursoResponse>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result is null)
                return Result.Failure<GetCursoResponse>("Curso no encontrado.");
            return Result.Success(result.Adapt<GetCursoResponse>());
        }

        public async Task<Result> UpdateAsync(int id, UpdateCursoRequest request)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result is null)
                return Result.Failure("Curso no encontrado.");
            request.Adapt(result);
            await _repository.UpdateAsync();
            return Result.Success("Curso actualizado exitosamente.");
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result is null)
                return Result.Failure("Curso no encontrado.");
            await _repository.DeleteAsync(id);
            return Result.Success("Curso eliminado exitosamente.");
        }

        public async Task<Result<List<ListCursosResponse>>> ListAsync(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _repository.ListAsync(
                predicate: c => c.Status,
                selector: c => new ListCursosResponse
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    Especialidad = c.Especialidad!.Nombre,
                    Precio = c.Precio,
                    CuposDisponibles = c.CuposDisponibles,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = c.CreatedBy
                },
                pageNumber: pageNumber,
                pageSize: pageSize
            );
            return Result.Success(result.Result.ToList());
        }
    }
}