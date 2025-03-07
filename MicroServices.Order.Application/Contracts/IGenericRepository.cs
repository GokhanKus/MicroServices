using System.Linq.Expressions;

namespace MicroServices.Order.Application.Contracts
{
	public interface IGenericRepository<TId, TEntity> where TEntity : class where TId : struct //TId alanina yanlıslıkla class tanimlanmasin TId guid, double int vs olabilir(valuetype)
	{
		Task<List<TEntity>> GetAllAsync();
		Task<List<TEntity>> GetAllPagedAsync(int pageNumber,int pageSize);
		ValueTask<TEntity> GetByIdAsync(TId id);
		Task<bool> AnyAsync(TId id);
		Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
		IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
		void Add(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
	}
}
