using Academia.DataAccess;
using Academia.DataAccess.Entities;
using Academia.Repositories.Interfaces;

namespace Academia.Repositories.Implementations
{
    public class MatriculaRepository(AcademiaDbContext context) : BaseRepository<Matricula>(context), IMatriculaRepository { }
}