

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class PropietarioRepository:GenericRepository<Propietario>,IPropietario
    {
      

    public PropietarioRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<Propietario> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Propietarios as IQueryable<Propietario>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.NombrePropietario.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                            /*      .Include(u => u.Mascotas) */
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }



    }
}