

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class MascotaRepository:GenericRepository<Mascota>,IMascota
    {
      

    public MascotaRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<Mascota> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Mascotas as IQueryable<Mascota>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.NombreMascota.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Include(u => u.Propietarios) 
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }


     public async Task<dynamic>  TraerMascotasPorEspecieConsulta1()
     {
        
                          

                      return  _context.Mascotas
                                    .GroupBy(m => m.Razas.Especies.NombreEspecie)
                                    .ToDictionary
                                    (
                                        group => group.Key
                                        

                                    ) ;
                 
                           
     }



    public async Task<dynamic> ListarMascotasAtendidasPorVeterinario_Consulta3(string NombreVeterinario)
    {
        var VeterinarioBuscado = await _context.Veterinarios.FirstOrDefaultAsync( x => x.VeterinarioNombre.ToLower() == NombreVeterinario.ToLower());
        if (VeterinarioBuscado == null) return "Veterinario no encontrado";



     return    _context.Citas.Where(x => x.VeterinarioId == VeterinarioBuscado.Id).Include(x => x.Mascotas)
                                .Select(e => new 
                                {
                                    VeterinarioBuscado = VeterinarioBuscado.VeterinarioNombre,
                                    MascotaNombre = e.Mascotas.NombreMascota
                                });
    
    }

    }
}