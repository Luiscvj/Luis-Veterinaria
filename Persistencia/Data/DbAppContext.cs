using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Persistencia.Seed;

namespace Persistencia.Data;

    public class DbAppContext : DbContext
    {
        public DbAppContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRoles> UsuariosRoles { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<MovimientoMedicamento> MovimientosMedicamentos { get; set; }
        public DbSet<MedicamentoProveedor> MedicamentosProveedores { get; set; }
        public DbSet<Propietario> Propietarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Raza> Razas { get; set; }
        public DbSet<TipoMovimiento> TiposMovimientos { get; set; }
        public DbSet<TratamientoMedico> TratamientosMedicos { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
      
        
    

        

         protected override void ConfigureConventions(ModelConfigurationBuilder modelBuilder)
         {
            modelBuilder.Properties<string>().HaveMaxLength(100);
         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SeedingInitial.Seed(modelBuilder);
        }
    }
/* 

Consultas B


 */