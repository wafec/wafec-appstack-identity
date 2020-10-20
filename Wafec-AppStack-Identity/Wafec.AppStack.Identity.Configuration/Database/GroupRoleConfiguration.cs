using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class GroupRoleConfiguration : EntityTypeConfiguration<GroupRole>
    {
        public GroupRoleConfiguration()
        {
            ToTable("GROUP_ROLE");
            HasKey(m => m.Id);

            Property(m => m.Id).HasColumnName("id");
            Property(m => m.RoleId).HasColumnName("role_id");
            Property(m => m.GroupId).HasColumnName("group_id");
            Property(m => m.Deleted).HasColumnName("deleted");
            HasRequired(m => m.Role).WithMany().HasForeignKey(m => m.RoleId);
            HasRequired(m => m.Group).WithMany().HasForeignKey(m => m.GroupId);
        }
    }
}
