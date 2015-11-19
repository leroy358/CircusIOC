using CircusIOC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CircusIOC.Repository
{
    public class Repository<TEntity> where TEntity:class
    {
        public MaXiDbContext DbContext { get; private set; }
        public DbSet<TEntity> DbSet { get; private set; }
        public Repository(MaXiDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }
        public IEnumerable<TEntity> Get()
        {
            return this.DbSet.AsQueryable();
        }
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return this.DbSet.Where(filter).AsQueryable();
        }
        public IEnumerable<TEntity> Get<TKey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> sortKeySelector, bool isAsc = true)
        {
            if (isAsc)
            {
                return this.DbSet.Where(filter).OrderBy(sortKeySelector).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                return this.DbSet.Where(filter).OrderByDescending(sortKeySelector).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
        }
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate).Count();
        }
        public void Add(TEntity instance)
        {
            this.DbSet.Add(instance);
            this.DbContext.SaveChanges();
        }
        public void Update(TEntity instance)
        {
            this.DbContext.Entry(instance).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }
        public void Delete(TEntity instance)
        {
            this.DbSet.Remove(instance);
            this.DbContext.SaveChanges();
        }
        public void Dispose()
        {
            this.DbContext.Dispose();
        }
    }
}