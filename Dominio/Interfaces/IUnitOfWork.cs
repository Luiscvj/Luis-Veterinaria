

namespace Dominio.Interfaces;

    public interface IUnitOfWork
    {
         IUsuario Usuarios {get;}
         IRol Roles {get;}
         ICita Citas {get;}
         IEspecie Especies {get;}
         ILaboratorio   Laboratorios {get;}
         IMascota   Mascotas {get;}
         IMedicamento Medicamentos  {get;}
         IMedicamentoProveedor MedicamentosProveedores {get;}
         IMovimientoMedicamento MovimientosMedicamentos {get;}
         IPropietario Propietarios  {get;}
         IProveedor Proveedores {get;}
         IRaza Razas {get;}
         ITipoMovimiento TipoMovimientos {get;}
         ITratamientoMedico TratamientosMedicos {get;}
         IVeterinario Veterinarios {get;}
        Task<int> SaveAsync();
    }

