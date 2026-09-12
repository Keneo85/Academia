using Academia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.DataAccess.Configurations
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Descripcion).HasMaxLength(500);
            builder.Property(c => c.Precio).HasPrecision(10, 2);
            builder.HasOne(c => c.Especialidad)
                .WithMany(e => e.Cursos)
                .HasForeignKey(c => c.EspecialidadId);
        }
    }
}