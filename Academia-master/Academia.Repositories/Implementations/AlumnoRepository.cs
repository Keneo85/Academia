using Academia.Common.Helpers;
using Academia.DataAccess;
using Academia.DataAccess.Entities;
using Academia.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Academia.Repositories.Implementations
{
    public class AlumnoRepository(AcademiaDbContext context) : BaseRepository<Alumno>(context), IAlumnoRepository
    {
        public async Task<Result> CreateWithUserAsync(Alumno alumno, User user)
        {
            using var trx = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await _context.Set<User>().AddAsync(user);
                await _context.SaveChangesAsync();
                alumno.UserId = result.Entity.Id;
                await CreateAsync(alumno);
                await trx.CommitAsync();
                return Result.Success("Alumno y usuario creados exitosamente.");
            }
            catch (Exception ex)
            {
                await trx.RollbackAsync();
                return Result.Failure($"Error al registrar alumno: {ex.Message}");
            }
        }

        public async Task<bool> UserExistsAsync(string userName)
            => await _context.Set<User>().AnyAsync(u => u.UserName == userName);
    }
}