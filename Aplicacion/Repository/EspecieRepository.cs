

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class EspecieRepository:GenericRepository<Especie>,IEspecie
    {
      

    public EspecieRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<Especie> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Especies as IQueryable<Especie>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.NombreEspecie.ToLower().Contains(search));
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