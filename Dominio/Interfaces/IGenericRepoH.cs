

namespace Dominio.Interfaces;
using System.Linq.Expressions;
using Dominio.Entities;

public interface IGenericRepositoryH_I<T> 
{
    Task<MovimientoMedicamento> GetByIdAsync(int IdPK1,int IdPK2);
      Task<MedicamentoProveedor> GetByIdAsyncProveedorMedicamento(int MedicamentoId, int ProveedorId);
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize);
    void Add(T  entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T  entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}