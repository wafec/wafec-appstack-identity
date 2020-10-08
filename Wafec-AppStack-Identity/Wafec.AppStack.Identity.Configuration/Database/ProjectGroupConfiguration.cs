using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class ProjectGroupConfiguration : EntityTypeConfiguration<ProjectGroup>
    {
        public ProjectGroupConfiguration()
        {
            ToTable("PROJECT_GROUP");
            HasKey(m => m.Id);

            Property(m => m.Id).HasColumnName("id");
            Property(m => m.GroupId).HasColumnName("group_id");
            Property(m => m.ProjectId).HasColumnName("project_id");
            HasRequired(m => m.Group).WithMany().HasForeignKey(m => m.GroupId);
            HasRequired(m => m.Project).WithMany().HasForeignKey(m => m.ProjectId);
        }
    }
}
