
using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IMascota : IGenericRepository<Mascota>
    {
        Task<dynamic>  TraerMascotasPorEspecieConsulta1();
        Task<dynamic> ListarMascotasAtendidasPorVeterinario_Consulta3(string NombreVeterinario);
         Task<dynamic> ListarMascotas_PropietariosConGoldenRetriever_Consulta5();
        Task<dynamic> ListarNumeroDeMascotasPorRaza();
    }
