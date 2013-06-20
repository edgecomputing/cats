using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Cats.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {

        public GenericRepository(CatsContext context)
        {
            ctx = context;
        }
        private CatsContext ctx;
        public CatsContext db
        {

            get { return ctx; }
            set { ctx = value; }
        }



        public virtual List<T> GetAll()
        {

            IQueryable<T> query = ctx.Set<T>();
            return query.ToList();
        }

        public List<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = ctx.Set<T>().Where(predicate);
            return query.ToList();
        }
        public virtual void Attach(T entity)
        {
            ctx.Set<T>().Attach(entity);
        }

        public virtual bool Add(T entity)
        {
            ctx.Set<T>().Add(entity);
            return true;
        }

        public virtual bool Delete(T entity)
        {
            ctx.Set<T>().Remove(entity);
            return true;
        }

        public virtual bool Edit(T entity)
        {
            ctx.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public virtual bool Save()
        {
            ctx.SaveChanges();
            return true;
        }

        public virtual bool SaveChanges(T entity)
        {
            if (ctx.Entry(entity).State == EntityState.Detached)
            {
                ctx.Set<T>().Attach(entity);
            }
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }
        public virtual T FindById(int id)
        {
            return ctx.Set<T>().Find(id);
        }

    }
}
