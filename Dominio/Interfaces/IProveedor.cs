
using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IProveedor : IGenericRepository<Proveedor>
    {
         Task<List<string>>  ListarProveedoresPorMedicamentoDeterminado_Consulta4(string NombreMedicamento);
    }
