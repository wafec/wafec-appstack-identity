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
        public DbSet<User> UsetSet { get; set; }
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
            return new Transaction(this.Database.BeginTransaction());
        }

        public IEnumerable<T> GetSet<T>() where T : class
        {
            return this.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
        }

        public class Transaction : ITransaction
        {
            public DbContextTransaction DbTransaction { get; private set; }

            public Transaction(DbContextTransaction dbTransaction)
            {
                DbTransaction = dbTransaction;
            }

            public void Commit()
            {
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
