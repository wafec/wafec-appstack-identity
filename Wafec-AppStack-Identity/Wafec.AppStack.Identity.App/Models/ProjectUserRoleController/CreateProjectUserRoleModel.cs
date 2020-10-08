using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.ProjectUserRoleController
{
    public class CreateProjectUserRoleModel
    {
        public long ProjectUserId { get; set; }
        public long ProjectRoleId { get; set; }
    }
}