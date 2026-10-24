using Academia.Business.DTO.Response.Especialidad;
using Academia.Business.Interfaces;
using Academia.Common.Helpers;
using Academia.Repositories.Interfaces;

namespace Academia.Business.Implementations
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IEspecialidadRepository _repository;
        public EspecialidadService(IEspecialidadRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ListEspecialidadesResponse>>> ListAsync()
        {
            var result = await _repository.ListAsync(
                predicate: e => e.Status,
                selector: e => new ListEspecialidadesResponse { Id = e.Id, Nombre = e.Nombre },
                pageNumber: 1,
                pageSize: 1000
            );
            return Result.Success(result.Result.ToList());
        }
    }
}
