﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.GroupRoleController
{
    public class RemoveGroupRoleModel
    {
        public long GroupId { get; set; }
        public long RoleId { get; set; }
    }
}