
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistencia.Seed;
    public class SeedingInitial
    {
        public static void Seed(ModelBuilder modelBuilder)
        {

            var EspecieCanino = new Especie {Id = 1, NombreEspecie ="Canino"};
            var EspecieFelino = new Especie {Id = 2,NombreEspecie ="Felino"};

            var RazaGoldenRetriever = new Raza{Id = 1, RazaNombre ="GoldenRetriever" ,EspecieId = EspecieCanino.Id};
            var RazaPitbull = new Raza{Id = 2, RazaNombre="Pitbull",EspecieId= EspecieCanino.Id};
            var RazaSavannah = new Raza{Id = 3, RazaNombre="Savannah", EspecieId = EspecieFelino.Id};


            var LaboratorioGemfar = new Laboratorio{Id =1 , LaboratorioNombre ="Gemfar" , LaboratiorTelefono ="24234234", LaboratorioDireccion="cra 19"};
            var LaboratorioMK = new Laboratorio{Id =2 , LaboratorioNombre ="MK" , LaboratiorTelefono ="12121212", LaboratorioDireccion="cra 1"};

            var Proveedor1 = new Proveedor{Id = 1,NombreProveedor="Proveedor1"};
            var Proveedor2 = new Proveedor{Id = 2,NombreProveedor="Proveedor2"};

            var MedicamentoAntifungico = new Medicamento{Id = 1, NombreMedicamento="Anti Fungico", CantidadDisponible=22,PrecioMedicamento=1200,LaboratorioId= LaboratorioGemfar.Id};
            var MedicamentoAntiPulgas = new Medicamento{Id = 2, NombreMedicamento="Anti Pulgas", CantidadDisponible=100,PrecioMedicamento=2000,LaboratorioId= LaboratorioMK.Id};


            var tipo_movimientoCompraProveedor = new TipoMovimiento {Id = 1 , DescripccionMovimiento ="Compra de medicamentos al proveedor"};
            var tipo_movimientoVenta = new TipoMovimiento {Id = 2 , DescripccionMovimiento ="Venta de medicamentos"};

            var movimientoMedicamento1 = new MovimientoMedicamento {MedicamentoId = MedicamentoAntifungico.Id,TipoMovimientoId = tipo_movimientoCompraProveedor.Id,PrecioMovimiento = 23213,CantidadMovida =3000,FechaMovimiento = new DateTime(2020,1,4)};
            var movimientoMedicamento2 = new MovimientoMedicamento {MedicamentoId = MedicamentoAntiPulgas.Id,TipoMovimientoId = tipo_movimientoVenta.Id,PrecioMovimiento = 900000,CantidadMovida =25000,FechaMovimiento = new DateTime(2023,2,4)};
           
            var veterinarioJorge = new Veterinario {Id =1,VeterinarioNombre="Jorge", VeterinarioEmail="jorge@gmail", VeterinarioTelefono ="2121",Especialidad="Cirugia"};

            var veterinarioLuis = new Veterinario {Id =2,VeterinarioNombre="Luis", VeterinarioEmail="Luis@gmail", VeterinarioTelefono ="4141",Especialidad="Medicamentos"};

            var propietario1 = new Propietario {Id = 1,NombrePropietario="Antonio", Email ="Antonio@", Telefono ="11111"};
            var propietario2 = new Propietario {Id = 2,NombrePropietario="Miguel", Email ="Miguel@", Telefono ="22222"};
            var mascota1 = new Mascota {Id =1,NombreMascota ="Apólo" ,FechaNacimiento = new DateTime(2011,2,1),RazaId = RazaGoldenRetriever.Id,PropietarioId = propietario1.Id};
            var mascota2 = new Mascota {Id =2,NombreMascota ="Zeus" ,FechaNacimiento = new DateTime(2005,2,1),RazaId = RazaGoldenRetriever.Id,PropietarioId = propietario2.Id};
            var mascota3 = new Mascota {Id =3,NombreMascota ="Cookie" ,FechaNacimiento = new DateTime(2017,2,1),RazaId = RazaSavannah.Id,PropietarioId = propietario2.Id};

            var MedicamentoProveedor = new MedicamentoProveedor {ProveedorId= Proveedor1.Id, MedicamentoId= MedicamentoAntifungico.Id};
            var cita1 = new Cita {Id = 1,FechaCita = new DateTime(2023,2,4),Motivo ="Dolor estomacal",MascotaId = mascota1.Id,VeterinarioId= veterinarioJorge.Id};
            var cita2 = new Cita {Id = 2,FechaCita = new DateTime(2023,2,9),Motivo ="Pulgas",MascotaId = mascota2.Id,VeterinarioId= veterinarioLuis.Id};

            

            modelBuilder.Entity<Especie>().HasData(EspecieCanino, EspecieFelino);
            modelBuilder.Entity<Raza>().HasData(RazaGoldenRetriever, RazaPitbull, RazaSavannah);
            modelBuilder.Entity<Laboratorio>().HasData(LaboratorioGemfar,LaboratorioMK);
            modelBuilder.Entity<Proveedor>().HasData(Proveedor1,Proveedor2);
            modelBuilder.Entity<Medicamento>().HasData(MedicamentoAntifungico,MedicamentoAntiPulgas);
            modelBuilder.Entity<TipoMovimiento>().HasData(tipo_movimientoCompraProveedor,tipo_movimientoVenta);
            modelBuilder.Entity<MovimientoMedicamento>().HasData(movimientoMedicamento1,movimientoMedicamento2);
            modelBuilder.Entity<Veterinario>().HasData(veterinarioJorge,veterinarioLuis);
            modelBuilder.Entity<Propietario>().HasData(propietario1,propietario2);
            modelBuilder.Entity<Mascota>().HasData(mascota1,mascota2,mascota3);
            modelBuilder.Entity<Cita>().HasData(cita1,cita2);
            modelBuilder.Entity<MedicamentoProveedor>().HasData(MedicamentoProveedor);
        }
    }

