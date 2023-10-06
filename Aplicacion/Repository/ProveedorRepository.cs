

using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class ProveedorRepository:GenericRepository<Proveedor>,IProveedor
    {
      

    public ProveedorRepository(DbAppContext context) : base(context)
    {
    
    }


       public override async Task<(int totalRegistros,IEnumerable<Proveedor> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Proveedores as IQueryable<Proveedor>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.NombreProveedor.ToLower().Contains(search));
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


     public async Task<List<string>>  ListarProveedoresPorMedicamentoDeterminado_Consulta4(string NombreMedicamento)
     {       
            List<String> Proveedores = new List<String>();
            var MedicamentoBuscado =await  _context.Medicamentos.FirstOrDefaultAsync( x => x.NombreMedicamento.ToLower() == NombreMedicamento.ToLower());


           var MedicamentoProveedor=  _context.MedicamentosProveedores.Where(x => x.MedicamentoId == MedicamentoBuscado.Id).Include(x => x.ProveedorId);


           foreach(var e in MedicamentoProveedor)
           {
             List<string> proveedor = await  _context.Proveedores.Where(x => x.Id == e.ProveedorId).Select(x => x.NombreProveedor).ToListAsync();
             Proveedores.AddRange(proveedor);
           }

           return Proveedores;

           
     }



    }
}