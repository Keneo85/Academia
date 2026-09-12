using Academia.DataAccess;
using Academia.DataAccess.Entities;
using Academia.Repositories.Implementations;
using Academia.Repositories.Interfaces;

namespace Academia.Repositories.Implementations
{
    public class CursoRepository(AcademiaDbContext context) : BaseRepository<Curso>(context), ICursoRepository { }
}