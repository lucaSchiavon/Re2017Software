using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using Ls.Base.Entity;
using Ls.Base.EF;
using AutoMapper;

namespace Ls.Base.EFRepository
{
    public abstract class GenericEFRepository<C,T> : IGenericEFRepository<T> , IDisposable where T: LsEntity where C : LsDbContext, new()
    {
        private C _entities = new C();

       

        public GenericEFRepository(string Username)
            {
            _entities.SetCurrentUser(Username);
            }
        public C Context {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll() {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }
        
        public IQueryable<T> FindBy(Expression<Func<T, bool>> Predictate) {
            IQueryable<T> query = _entities.Set<T>().Where(Predictate);
            return query;
        }

        public virtual void Add(T Entity)
        {
            _entities.Set<T>().Add(Entity);
        }

        public virtual void Delete(T Entity)
        {
            _entities.Set<T>().Remove(Entity);
        }

        public virtual void Edit(T Entity)
        {
            _entities.Entry(Entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Save() {
            _entities.SaveChanges();
        }

      
        //public T GetSingle(int id)
        //{
        //    return this.Context.Set<T>().Single(x => x.Id == id);
           
        //}
       
        protected virtual void _setObjectsMapperConfig()
        {
        }

        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _entities.Dispose();
                    //<C>.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public virtual void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

    }
}
