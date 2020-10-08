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
        }
    }
}
