using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class PasswordLevelConfiguration : EntityTypeConfiguration<PasswordLevel>
    {
        public PasswordLevelConfiguration()
        {
            ToTable("PASSWORD_LEVEL");
            HasKey(m => m.Id);

            Property(m => m.Id).HasColumnName("id");
            Property(m => m.Name).HasColumnName("name");
        }
    }
}
