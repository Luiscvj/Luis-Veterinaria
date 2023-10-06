using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class EspecieConfiguration : IEntityTypeConfiguration<Especie>
    {
        public void Configure(EntityTypeBuilder<Especie> builder)
        {
            // Configure entity here
               builder.ToTable("especie");
               builder.Property(x => x.NombreEspecie)
                       .HasMaxLength(180);
               
               
        }
    }