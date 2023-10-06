

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class MedicamentoRepository:GenericRepository<Medicamento>,IMedicamento
    {
      

    public MedicamentoRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<Medicamento> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Medicamentos as IQueryable<Medicamento>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.NombreMedicamento.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                              /*   .Include(u => u.) */
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }

        public async Task<IEnumerable<Medicamento>> MedicamentosLaboratorio_Consulta2()
        {
            return await _context.Medicamentos.Where(l => l.LaboratorioId == 1).ToListAsync();
        }
    }
}