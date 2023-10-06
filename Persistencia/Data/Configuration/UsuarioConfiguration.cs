using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Configure entity here
               builder.ToTable("usuario");
               builder.HasMany(x => x.Roles)
                   .WithMany(x => x.Usuarios)
                   .UsingEntity<UsuarioRoles>();

              builder.HasMany(x => x.RefreshTokens)
                     .WithOne(u => u.Usuarios)
                     .HasForeignKey(r => r.UsuarioId);
               
        }
    }