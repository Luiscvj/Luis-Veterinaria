using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class TipoMovimientoConfiguration : IEntityTypeConfiguration<TipoMovimiento>
    {
        public void Configure(EntityTypeBuilder<TipoMovimiento> builder)
        {
            // Configure entity here
               builder.ToTable("tipo_movimiento");

               builder.Property(x => x.DescripccionMovimiento)
                       .HasMaxLength(200);
               
        }
    }