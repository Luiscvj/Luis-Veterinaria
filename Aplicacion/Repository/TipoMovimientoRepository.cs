

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class TipoMovimientoRepository:GenericRepository<TipoMovimiento>,ITipoMovimiento
    {
      

    public TipoMovimientoRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<TipoMovimiento> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.TiposMovimientos as IQueryable<TipoMovimiento>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.DescripccionMovimiento.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                /*  .Include(u => u.Mascotas)
                                 .ThenInclude(u => u.Propietarios)  */
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }



    }
}