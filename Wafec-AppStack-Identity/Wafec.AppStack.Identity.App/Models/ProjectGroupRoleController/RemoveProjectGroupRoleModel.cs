using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.ProjectGroupRoleController
{
    public class RemoveProjectGroupRoleModel
    {
        public long ProjectId { get; set; }
        public long GroupId { get; set; }
        public long RoleId { get; set; }
    }
}