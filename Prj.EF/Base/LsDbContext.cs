using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Ls.Base.Entity;
using System.Data.Entity.Validation;

namespace Ls.Base.EF
{
    public class LsDbContext : DbContext, ILsDbContext
    {
        private string _currentUsername;

        public LsDbContext(string ConStrName)
            : base("name=" + ConStrName)
        {
        }

        public void SetCurrentUser(string username)
        {
            _currentUsername = string.IsNullOrEmpty(username) ? "Anonymous" : username;
        }

        public override int SaveChanges()
        {
            //IEnumerable<DbEntityEntry> Entities = ChangeTracker.Entries().Where(x => x.Entity is LsEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            //foreach (var Entity in Entities)
            //{
            //    if (Entity.State == EntityState.Added)
            //    {
            //        ((LsEntity)Entity.Entity).DateCreated = DateTimeOffset.Now;
            //        ((LsEntity)Entity.Entity).UserCreated = _currentUsername;
            //        Entity.Property("DateModified").IsModified = false;
            //        Entity.Property("UserModified").IsModified = false;
            //    }
            //    else
            //    {
            //        Entity.Property("DateCreated").IsModified = false;
            //        Entity.Property("UserCreated").IsModified = false;
            //        ((LsEntity)Entity.Entity).DateModified = DateTimeOffset.Now;
            //        ((LsEntity)Entity.Entity).UserModified = _currentUsername;
            //    }
            //}

            return base.SaveChanges();
        }

        void ILsDbContext.OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        //??
        bool ILsDbContext.ShouldValidateEntity(DbEntityEntry entityEntry) => base.ShouldValidateEntity(entityEntry);
        //??
        DbEntityValidationResult ILsDbContext.ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
          => base.ValidateEntity(entityEntry, items);


        //private bool disposedValue = false; // To detect redundant calls
        //protected override void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            base.Dispose();
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        //        // TODO: set large fields to null.

        //        disposedValue = true;
        //    }
        //}
        //public new void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(true);
        //    // TODO: uncomment the following line if the finalizer is overridden above.
        //    // GC.SuppressFinalize(this);
        //}

    }

}
