using System.Linq.Expressions;

namespace IOT.Application.Contract.Persistence
{
	public interface IRepository<T, Key> where T : class
	{
		Task<T> GetByIdAsync(Key id);
		Task<IEnumerable<T>> GetAllAsync();
		IEnumerable<T> Find(Expression<Func<T, bool>> expression);
		void Update(T entity);
		void Add(T entity);
		void AddSyn(T entity);
		//(synchronous
		void AddRange(IEnumerable<T> entities);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
