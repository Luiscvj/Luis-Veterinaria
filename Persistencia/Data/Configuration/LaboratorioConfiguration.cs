using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class LaboratorioConfiguration : IEntityTypeConfiguration<Laboratorio>
    {
        public void Configure(EntityTypeBuilder<Laboratorio> builder)
        {
            // Configure entity here
               builder.ToTable("laboratorio");
               builder.Property(x => x.LaboratiorTelefono)
                       .HasMaxLength(20);
               
        }
    }