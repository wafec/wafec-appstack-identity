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
        public DbSet<Role> RoleSet { get; set; }
        public DbSet<Group> GroupSet { get; set; }
        public DbSet<UserRole> UserRoleSet { get; set; }
        public DbSet<UserGroup> UserGroupSet { get; set; }
        public DbSet<GroupRole> GroupRoleSet { get; set; }
        public DbSet<ProjectUser> ProjectUserSet { get; set; }
        public DbSet<ProjectRole> ProjectRoleSet { get; set; }
        public DbSet<ProjectGroup> ProjectGroupSet { get; set; }
        public DbSet<ProjectGroupRole> ProjectGroupRoleSet { get; set; }
        public DbSet<ProjectUserRole> ProjectUserRoleSet { get; set; }

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
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new UserGroupConfiguration());
            modelBuilder.Configurations.Add(new GroupRoleConfiguration());
            modelBuilder.Configurations.Add(new ProjectUserConfiguration());
            modelBuilder.Configurations.Add(new ProjectGroupConfiguration());
            modelBuilder.Configurations.Add(new ProjectRoleConfiguration());
            modelBuilder.Configurations.Add(new ProjectUserRoleConfiguration());
            modelBuilder.Configurations.Add(new ProjectGroupRoleConfiguration());
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
