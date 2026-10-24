using Academia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.DataAccess.Configurations
{
    public class AlumnoConfiguration : IEntityTypeConfiguration<Alumno>
    {
        public void Configure(EntityTypeBuilder<Alumno> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Nombres).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Apellidos).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(200);
            builder.HasIndex(a => a.Email).IsUnique();
            builder.Property(a => a.Telefono).IsRequired().HasMaxLength(20);
            builder.Property(a => a.DNI).IsRequired().HasMaxLength(8);
            builder.HasIndex(a => a.DNI).IsUnique();
            builder.HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Alumno>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}