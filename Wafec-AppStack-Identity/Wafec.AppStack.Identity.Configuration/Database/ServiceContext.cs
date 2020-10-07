using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class ServiceContext : DbContext, IRepository
    {
        public DbSet<User> UserSet { get; set; }
        public DbSet<Project> ProjectSet { get; set; }

        public ServiceContext() : base("ServiceContext")
        {
            
        }

        public T Add<T>(T obj) where T : class
        {
            return this.Set<T>().Add(obj);
        }

        public ITransaction BeginTransaction()
        {
            return new Transaction(this, this.Database.BeginTransaction());
        }

        public IEnumerable<T> GetSet<T>() where T : class
        {
            return this.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
        }

        void IRepository.SaveChanges()
        {
            base.SaveChanges();
        }

        public class Transaction : ITransaction
        {
            public ServiceContext DbContext { get; private set; }
            public DbContextTransaction DbTransaction { get; private set; }

            public Transaction(ServiceContext dbContext, DbContextTransaction dbTransaction)
            {
                DbContext = dbContext;
                DbTransaction = dbTransaction;
            }

            public void Commit()
            {
                DbContext.SaveChanges();
                DbTransaction.Commit();
            }

            public void Dispose()
            {
                DbTransaction.Dispose();
            }

            public void Rollback()
            {
                DbTransaction.Rollback();
            }
        }
    }
}
