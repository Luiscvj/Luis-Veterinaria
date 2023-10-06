

using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    RolRepository _rol;
    UsuarioRepository _usuario;
    CitaRepository _cita;
    EspecieRepository _especie;
    LaboratorioRepository   _laboratorio;
    MascotaRepository _mascota;
    MedicamentoRepository _medicamento;
    MedicamentoProveedorRepository _medicamentoProveedor;
    MovimientoMedicamentoRepository _movimientoMedicamento;
    PropietarioRepository _propietario;
    ProveedorRepository _proveedor;
    RazaRepository _raza;
    TipoMovimientoRepository _tipoMovimiento;
    TratamientoMedicoRepository _tratamientoMedico;
    VeterinarioRepository _veterinario;
    private readonly DbAppContext _context;
    public UnitOfWork(DbAppContext context)
    {
        _context = context;
    }
    public IUsuario Usuarios
    {
        get
        {
            if (_usuario is not null)
            {
                return _usuario;
            }
            return _usuario = new UsuarioRepository(_context);
        }
    }
    public IRol Roles
    {
        get
        {
            if (_rol is not null)
            {
                return _rol;
            }
            return _rol = new RolRepository(_context);
        }
    }

    public ICita Citas
    {
        get
        {
            if (_cita == null)
            {
                _cita = new CitaRepository(_context);
            }
            return _cita;
        }
    }

    public IEspecie Especies
    {
        get
        {
            if (_especie == null)
            {
                _especie = new EspecieRepository(_context);
            }
            return _especie;
        }
    }

   public ILaboratorio Laboratorios
   {
       get
       {
           if (_laboratorio == null)
           {
               _laboratorio = new LaboratorioRepository(_context);
           }
           return _laboratorio;
       }
   }

   public IMascota Mascotas
   {
       get
       {
           if (_mascota == null)
           {
               _mascota = new MascotaRepository(_context);
           }
           return _mascota;
       }
   }

  public IMedicamento Medicamentos
  {
      get
      {
          if (_medicamento == null)
          {
              _medicamento = new MedicamentoRepository(_context);
          }
          return _medicamento;
      }
  }

   public IMedicamentoProveedor MedicamentosProveedores
   {
       get
       {
           if (_medicamentoProveedor == null)
           {
               _medicamentoProveedor = new MedicamentoProveedorRepository(_context);
           }
           return _medicamentoProveedor;
       }
   }

    public IMovimientoMedicamento MovimientosMedicamentos
    {
        get
        {
            if (_movimientoMedicamento == null)
            {
                _movimientoMedicamento = new MovimientoMedicamentoRepository(_context);
            }
            return _movimientoMedicamento;
        }
    }

    public IPropietario Propietarios
    {
        get
        {
            if (_propietario == null)
            {
                _propietario = new PropietarioRepository(_context);
            }
            return _propietario;
        }
    }

    public IProveedor Proveedores
    {
        get
        {
            if (_proveedor == null)
            {
                _proveedor = new ProveedorRepository(_context);
            }
            return _proveedor;
        }
    }

   public IRaza Razas
   {
       get
       {
           if (_raza == null)
           {
               _raza = new RazaRepository(_context);
           }
           return _raza;
       }
   }

  public ITipoMovimiento TipoMovimientos
  {
      get
      {
          if (_tipoMovimiento == null)
          {
              _tipoMovimiento = new TipoMovimientoRepository(_context);
          }
          return _tipoMovimiento;
      }
  }

    public ITratamientoMedico TratamientosMedicos
    {
        get
        {
            if (_tratamientoMedico == null)
            {
                _tratamientoMedico = new TratamientoMedicoRepository(_context);
            }
            return _tratamientoMedico;
        }
    }

    public IVeterinario Veterinarios
    {
        get
        {
            if (_veterinario == null)
            {
                _veterinario = new VeterinarioRepository(_context);
            }
            return _veterinario;
        }
    }


    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

}