﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.ProjectRoleController
{
    public class CreateProjectRoleModel
    {
        public long ProjectId { get; set; }
        public long RoleId { get; set; }
    }
}