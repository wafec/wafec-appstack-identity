using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.ProjectGroupRoleController
{
    public class CreateProjectGroupRoleModel
    {
        public long ProjectGroupId { get; set; }
        public long ProjectRoleId { get; set; }
    }
}