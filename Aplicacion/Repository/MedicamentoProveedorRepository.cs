

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class MedicamentoProveedorRepository: GenericRepositoryH_I<MedicamentoProveedor>,IMedicamentoProveedor
    {
      

    public MedicamentoProveedorRepository(DbAppContext context) : base(context)
    {
    
    }


       public  async Task<(int totalRegistros,IEnumerable<MedicamentoProveedor> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.MedicamentosProveedores as IQueryable<MedicamentoProveedor>;
      

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