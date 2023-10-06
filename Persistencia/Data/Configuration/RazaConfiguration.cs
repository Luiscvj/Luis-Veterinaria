using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class RazaConfiguration : IEntityTypeConfiguration<Raza>
    {
        public void Configure(EntityTypeBuilder<Raza> builder)
        {
            // Configure entity here
               builder.ToTable("raza");

               builder.Property(x => x.RazaNombre)
                       .HasMaxLength(180);

               builder.HasOne(x => x.Especies)
                   .WithMany(x => x.Razas)
                   .HasForeignKey(x => x.EspecieId);
               
               
        }
    }