using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.ProjectUserRoleController
{
    public class RemoveProjectUserRoleModel
    {
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}