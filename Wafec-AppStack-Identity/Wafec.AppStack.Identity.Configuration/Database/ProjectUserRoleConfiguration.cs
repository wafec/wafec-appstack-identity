﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Configuration.Database
{
    public class ProjectUserRoleConfiguration : EntityTypeConfiguration<ProjectUserRole>
    {
        public ProjectUserRoleConfiguration()
        {
            ToTable("PROJECT_USER_ROLE");
            HasKey(m => m.Id);

            Property(m => m.Id).HasColumnName("id");
            Property(m => m.ProjectRoleId).HasColumnName("project_role_id");
            Property(m => m.ProjectUserId).HasColumnName("project_user_id");
            HasRequired(m => m.ProjectRole).WithMany().HasForeignKey(m => m.ProjectRoleId);
            HasRequired(m => m.ProjectUser).WithMany().HasForeignKey(m => m.ProjectUserId);
        }
    }
}
