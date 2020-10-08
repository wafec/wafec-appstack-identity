﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            ToTable("USER_ROLE");
            HasKey(m => m.Id);

            Property(m => m.Id).HasColumnName("id");
            Property(m => m.RoleId).HasColumnName("role_id");
            Property(m => m.UserId).HasColumnName("user_id");
            HasRequired(m => m.Role).WithMany().HasForeignKey(m => m.RoleId);
            HasRequired(m => m.User).WithMany().HasForeignKey(m => m.UserId);
        }
    }
}
