

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class LaboratorioRepository:GenericRepository<Laboratorio>,ILaboratorio
    {
      

    public LaboratorioRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<Laboratorio> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Laboratorios as IQueryable<Laboratorio>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.LaboratorioNombre.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(u => u.Medicamentos) 
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }



    }
}