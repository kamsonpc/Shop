using SimpleShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleShop.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly DbContext Contex;

		public Repository(DbContext context)
		{
			Contex = context;
		}

		public IEnumerable<TEntity> GetAll(string includeProperties = "")
		{
			IQueryable<TEntity> query = Contex.Set<TEntity>();

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			return query;
		}

		public TEntity Get(int id)
		{
			return Contex.Set<TEntity>().Find(id);
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{

			return Contex.Set<TEntity>().Where(predicate);
		}

		public void Add(TEntity entity)
		{
			Contex.Set<TEntity>().Add(entity);
		}

		public void AddRange(IEnumerable<TEntity> entities)
		{
			Contex.Set<TEntity>().AddRange(entities);
		}

		public void Remove(TEntity entity)
		{
			Contex.Set<TEntity>().Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			Contex.Set<TEntity>().RemoveRange(entities);
		}
	}
}