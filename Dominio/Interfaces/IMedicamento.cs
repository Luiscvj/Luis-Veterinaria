
using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IMedicamento : IGenericRepository<Medicamento>
    {
        Task<IEnumerable<Medicamento>> MedicamentosLaboratorio_Consulta2();
    }
