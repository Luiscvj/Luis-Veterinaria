using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class CitaConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            // Configure entity here
               builder.ToTable("cita");

               builder.HasOne(x => x.Mascotas)
                   .WithMany(x => x.Citas)
                   .HasForeignKey(x => x.MascotaId);
                   
               builder.HasOne(x => x.Veterinarios)
                   .WithMany(x => x.Citas)
                   .HasForeignKey(x => x.VeterinarioId);
            
               
        }
    }