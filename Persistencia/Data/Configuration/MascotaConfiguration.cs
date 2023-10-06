using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
    {
        public void Configure(EntityTypeBuilder<Mascota> builder)
        {
            // Configure entity here
               builder.ToTable("mascota");

               builder.HasOne(x => x.Propietarios)
                   .WithMany(x => x.Mascotas)
                   .HasForeignKey(x => x.PropietarioId);

             

               builder.HasOne(x => x.Razas)
                   .WithMany(x => x.Mascotas)
                   .HasForeignKey(x => x.RazaId);
               
               
               

        }
    }