using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Service
{
    public class ServiceContext : DbContext
    {
        public DbSet<User> UserSet { get; set; }
        public DbSet<Project> ProjectSet { get; set; }
        public DbSet<Group> GroupSet { get; set; }
        public DbSet<Role> RoleSet { get; set; }
        public DbSet<ProjectUser> ProjectUserSet { get; set; }
        public DbSet<ProjectRole> ProjectRoleSet { get; set; }
        public DbSet<ProjectGroup> ProjectGroupSet { get; set; }
        public DbSet<GroupRole> GroupRoleSet { get; set; }
        public DbSet<UserRole> UserRoleSet { get; set; }
        public DbSet<UserGroup> UserGroupSet { get; set; }
        public DbSet<ProjectGroupRole> ProjectGroupRoleSet { get; set; }
        public DbSet<ProjectUserRole> ProjectUserRoleSet { get; set; }
    }
}
