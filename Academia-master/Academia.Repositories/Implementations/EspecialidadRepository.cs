using Academia.DataAccess;
using Academia.DataAccess.Entities;
using Academia.Repositories.Interfaces;

namespace Academia.Repositories.Implementations
{
    public class EspecialidadRepository(AcademiaDbContext context) : BaseRepository<Especialidad>(context), IEspecialidadRepository { }
}
