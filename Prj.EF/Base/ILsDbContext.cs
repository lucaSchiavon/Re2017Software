using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ls.Base.EF
{
    public interface ILsDbContext
    {
        void SetCurrentUser(string UserName);
        //int SaveChanges();
        // DbEntityEntry Entry(object entity);
        //void OnModelCreating(DbModelBuilder modelBuilder);
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        Database Database { get; }
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        //void Dispose();
        void OnModelCreating(DbModelBuilder modelBuilder);
        bool ShouldValidateEntity(DbEntityEntry entityEntry);
        DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items);
    }
}
