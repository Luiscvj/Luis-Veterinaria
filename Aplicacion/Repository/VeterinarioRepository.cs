

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class VeterinarioRepository:GenericRepository<Veterinario>,IVeterinario
    {
      

    public VeterinarioRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<Veterinario> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Veterinarios as IQueryable<Veterinario>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.VeterinarioNombre.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 /* .Include(u => u.Mascotas)
                                 .ThenInclude(u => u.Propietarios)  */
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }



    }
}