using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            // Configure entity here
               builder.ToTable("medicamento");

               builder.Property(x => x.PrecioMedicamento)
                       .HasPrecision(10,3);

               builder.Property(x => x.NombreMedicamento)
                       .HasMaxLength(200);

                builder.HasOne(x => x.Laboratorios)
                    .WithMany(x => x.Medicamentos)
                    .HasForeignKey(x => x.LaboratorioId);

                builder.HasMany(x => x.TiposMovimientos)
                    .WithMany(x => x.Medicamentos)
                    .UsingEntity<MovimientoMedicamento>(

                         j=>{
                            j.HasOne(x =>x.TiposMovimientos)
                            .WithMany(x => x.MovimientosMedicamentos)
                            .HasForeignKey(x => x.TipoMovimientoId);
                            
                      
                        
                            j.HasOne(x => x.Medicamentos)
                                .WithMany(x => x.MovimientosMedicamentos)
                                .HasForeignKey(x => x.MedicamentoId);

                         } );
                
                
               
               
        }
    }