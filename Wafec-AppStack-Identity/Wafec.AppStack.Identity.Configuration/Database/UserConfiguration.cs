using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("USER");
            HasKey(u => u.Id);

            Property(m => m.Id).HasColumnName("id");
            Property(m => m.Name).HasColumnName("username");
            Property(m => m.Password).HasColumnName("passwd");
            Property(m => m.PasswordAlgorithmId).HasColumnName("password_algorithm_id");
            Property(m => m.PasswordLevelId).HasColumnName("password_level_id");
            HasRequired(m => m.PasswordAlgorithm).WithMany().HasForeignKey(m => m.PasswordAlgorithmId);
            HasRequired(m => m.PasswordLevel).WithMany().HasForeignKey(m => m.PasswordLevelId);
        }
    }
}
