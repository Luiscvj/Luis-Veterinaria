
using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IProveedor : IGenericRepository<Proveedor>
    {
         Task<List<String>>  ListarProveedoresPorMedicamentoDeterminado_Consulta4(string NombreMedicamento);
    }
