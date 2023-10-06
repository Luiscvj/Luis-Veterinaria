using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class VeterinarioConfiguration : IEntityTypeConfiguration<Veterinario>
    {
        public void Configure(EntityTypeBuilder<Veterinario> builder)
        {
            // Configure entity here
               builder.ToTable("veterinario");

               builder.Property(x => x.VeterinarioTelefono)
                       .HasMaxLength(20);
                
               
        }
    }