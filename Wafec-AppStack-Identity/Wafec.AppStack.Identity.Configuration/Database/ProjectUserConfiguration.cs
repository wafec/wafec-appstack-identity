using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class ProjectUserConfiguration : EntityTypeConfiguration<ProjectUser>
    {
        public ProjectUserConfiguration()
        {
            ToTable("PROJECT_USER");
            HasKey(m => m.Id);

            Property(m => m.Id).HasColumnName("id");
            Property(m => m.ProjectId).HasColumnName("project_id");
            Property(m => m.UserId).HasColumnName("user_id");
            HasRequired(m => m.Project).WithMany().HasForeignKey(m => m.ProjectId);
            HasRequired(m => m.User).WithMany().HasForeignKey(m => m.UserId);
        }
    }
}
