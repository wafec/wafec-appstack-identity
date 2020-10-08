using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class ProjectGroupRoleConfiguration : EntityTypeConfiguration<ProjectGroupRole>
    {
        public ProjectGroupRoleConfiguration()
        {
            ToTable("PROJECT_GROUP_ROLE");
            HasKey(m => m.Id);

            Property(m => m.Id).HasColumnName("id");
            Property(m => m.ProjectGroupId).HasColumnName("project_group_id");
            Property(m => m.ProjectRoleId).HasColumnName("project_role_id");
            HasRequired(m => m.ProjectGroup).WithMany().HasForeignKey(m => m.ProjectGroupId);
            HasRequired(m => m.ProjectRole).WithMany().HasForeignKey(m => m.ProjectRoleId);
        }
    }
}
