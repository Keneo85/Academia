using Academia.DataAccess;
using Academia.DataAccess.Entities;
using Academia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Academia.Repositories.Implementations
{
    public class MatriculaRepository(AcademiaDbContext context) : BaseRepository<Matricula>(context), IMatriculaRepository
    {
        public async Task<Matricula?> GetWithDetallesAsync(int id)
            => await _context.Set<Matricula>()
                .Include(m => m.Alumno)
                .Include(m => m.Detalles).ThenInclude(d => d.Curso)
                .FirstOrDefaultAsync(m => m.Status && m.Id == id);
    }
}
