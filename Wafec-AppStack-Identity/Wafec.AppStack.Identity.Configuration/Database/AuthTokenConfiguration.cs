using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class AuthTokenConfiguration : EntityTypeConfiguration<AuthToken>
    {
        public AuthTokenConfiguration()
        {
            ToTable("AUTH_TOKEN");
            HasKey(m => m.Id);

            Property(m => m.UserId).HasColumnName("user_id");
            Property(m => m.Token).HasColumnName("token");
            Property(m => m.Refresh).HasColumnName("refresh");
            Property(m => m.Expires).HasColumnName("expires");
            Property(m => m.Created).HasColumnName("created");
            HasRequired(m => m.User).WithMany().HasForeignKey(m => m.UserId);
        }
    }
}
