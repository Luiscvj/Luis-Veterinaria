using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository;


public class GenericRepositoryH_I<T> : IGenericRepositoryH_I<T> where T : class{
     protected  readonly DbAppContext _context;

    public GenericRepositoryH_I (DbAppContext context)
    {
        _context = context;
    }


    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
       _context.Set<T>().AddRange(entities);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
         return _context.Set<T>().Where(expression);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize)
    {
         var totalRegistros = await _context.Set<T>().CountAsync();
        var registros = await _context.Set<T>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }

    public virtual async Task<MovimientoMedicamento> GetByIdAsync(int MedicamentoId, int TipoMovimientoId)
    {
        return await _context.Set<MovimientoMedicamento>().Where(x=>x.MedicamentoId   == MedicamentoId && x.TipoMovimientoId == TipoMovimientoId).FirstOrDefaultAsync();
    }
    public virtual async Task<MedicamentoProveedor> GetByIdAsyncProveedorMedicamento(int MedicamentoId, int ProveedorId)
    {
        return await _context.Set<MedicamentoProveedor>().Where(x=>x.MedicamentoId   == MedicamentoId && x.ProveedorId == ProveedorId).FirstOrDefaultAsync();
    }

    public virtual void Remove(T entity)
    {
         _context.Set<T>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
       _context.Set<T>().RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
          _context.Set<T>()
            .Update(entity);
    }
}