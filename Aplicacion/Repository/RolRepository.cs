

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class RolRepository:GenericRepository<Rol>,IRol
    {
      

    public RolRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<Rol> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Roles as IQueryable<Rol>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

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