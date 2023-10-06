using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class MedicamentoProveedorConfiguration : IEntityTypeConfiguration<MedicamentoProveedor>
    {
        public void Configure(EntityTypeBuilder<MedicamentoProveedor> builder)
        {
            // Configure entity here
               builder.ToTable("medicamento_proveedor");
        }
    }