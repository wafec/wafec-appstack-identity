using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            ToTable("PROJECT");
            HasKey(m => m.Id);

            Property(m => m.Name).HasColumnName("name");
            Property(m => m.Description).HasColumnName("description");
            Property(m => m.OwnerId).HasColumnName("owner_id");
            HasRequired(m => m.Owner).WithMany().HasForeignKey(m => m.OwnerId);
        }
    }
}
