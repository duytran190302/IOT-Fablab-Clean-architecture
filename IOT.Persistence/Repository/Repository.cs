using IOT.Application.Contract.Persistence;
using IOT.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Persistence.Repository
{
	public class Repository<T, Key> : IRepository<T, Key> where T : class
	{

		private readonly DbSet<T> _dbSet;
		protected readonly IOTDbContext _context;
		public Repository(IOTDbContext context)
		{
			_dbSet = context.Set<T>();
			_context = context;
		}
		public void Add(T entity)
		{
			_dbSet.AddAsync(entity);
		}
		public void AddSyn(T entity)
		{
			_dbSet.Add(entity);
			_context.SaveChanges();
		}
		public void AddRange(IEnumerable<T> entities)
		{
			_dbSet.AddRange(entities);
		}
		public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
		{
			return _dbSet.Where(expression);
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}
		public async Task<T> GetByIdAsync(Key id)
		{
			return await _dbSet.FindAsync(id);
		}
		public void Remove(T entity)
		{
			_dbSet.Remove(entity);
		}
		public void RemoveRange(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}
	}
}
