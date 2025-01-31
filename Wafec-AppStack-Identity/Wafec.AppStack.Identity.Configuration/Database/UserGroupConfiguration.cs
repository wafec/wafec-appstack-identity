﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class UserGroupConfiguration : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupConfiguration()
        {
            ToTable("USER_GROUP");
            HasKey(m => m.Id);

            Property(m => m.Id).HasColumnName("id");
            Property(m => m.GroupId).HasColumnName("group_id");
            Property(m => m.UserId).HasColumnName("user_id");
            Property(m => m.Deleted).HasColumnName("deleted");
            HasRequired(m => m.Group).WithMany().HasForeignKey(m => m.GroupId);
            HasRequired(m => m.User).WithMany().HasForeignKey(m => m.UserId);
        }
    }
}
