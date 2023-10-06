using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            // Configure entity here
               builder.ToTable("proveedor");
               builder.Property(x => x.Telefono)
                       .HasMaxLength(20);

              builder.HasMany(x => x.Medicamentos)
                  .WithMany(x => x.Proveedores)
                  .UsingEntity<MedicamentoProveedor>();
               
        }
    }