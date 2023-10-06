

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class MovimientoMedicamentoRepository: GenericRepositoryH_I<MovimientoMedicamento>,IMovimientoMedicamento
    {
      

    public MovimientoMedicamentoRepository(DbAppContext context) : base(context)
    {
    
    }


       public  async Task<(int totalRegistros,IEnumerable<MovimientoMedicamento> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.MovimientosMedicamentos as IQueryable<MovimientoMedicamento>;
      

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                /* .Include(u => u.Departamentos) */
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }



    }
}