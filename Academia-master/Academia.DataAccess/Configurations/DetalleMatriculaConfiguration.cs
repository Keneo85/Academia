using Academia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.DataAccess.Configurations
{
    public class DetalleMatriculaConfiguration : IEntityTypeConfiguration<DetalleMatricula>
    {
        public void Configure(EntityTypeBuilder<DetalleMatricula> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.PrecioUnitario).HasPrecision(10, 2);
            builder.HasOne(d => d.Matricula)
                .WithMany(m => m.Detalles)
                .HasForeignKey(d => d.MatriculaId);
            builder.HasOne(d => d.Curso)
                .WithMany()
                .HasForeignKey(d => d.CursoId);
        }
    }
}