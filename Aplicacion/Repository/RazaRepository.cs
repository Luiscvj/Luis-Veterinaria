

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class RazaRepository:GenericRepository<Raza>,IRaza
    {
      

    public RazaRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<Raza> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Razas as IQueryable<Raza>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.RazaNombre.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                     /*             .Include(u => u.Mascotas)
                                 .ThenInclude(u => u.Propietarios)  */
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }



    }
}