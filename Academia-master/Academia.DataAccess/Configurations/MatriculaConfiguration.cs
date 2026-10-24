using Academia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.DataAccess.Configurations
{
    public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Total).HasPrecision(10, 2);
            builder.HasOne(m => m.Alumno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlumnoId);
        }
    }
}