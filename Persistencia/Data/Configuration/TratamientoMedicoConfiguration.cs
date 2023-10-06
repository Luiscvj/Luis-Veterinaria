using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class TratamientoMedicoConfiguration : IEntityTypeConfiguration<TratamientoMedico>
    {
        public void Configure(EntityTypeBuilder<TratamientoMedico> builder)
        {
            // Configure entity here
               builder.ToTable("tratamiento_medico");

               builder.Property(x => x.Dosis)
                       .HasPrecision(6,2);

               builder.Property(x => x.TipoUnidad)
                       .HasMaxLength(4);
               builder.Property(x => x.Observacion)
                       .HasMaxLength(250);
 
 
               builder.HasOne(x => x.Citas)
                     .WithMany(x => x.TratamientosMedicos)
                     .HasForeignKey(x => x.CitaId);

               builder.HasOne(x => x.Medicamentos)
                    .WithMany(x => x.TratamientosMedicos)
                    .HasForeignKey(x => x.MedicamentoId);
                
              
              
               
               

        }
    }