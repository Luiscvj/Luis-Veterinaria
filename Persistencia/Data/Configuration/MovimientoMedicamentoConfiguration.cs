using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class MovimientoMedicamentoConfiguration : IEntityTypeConfiguration<MovimientoMedicamento>
    {
        public void Configure(EntityTypeBuilder<MovimientoMedicamento> builder)
        {
            // Configure entity here
               builder.ToTable("movimiento_medicamento");
        }
    }