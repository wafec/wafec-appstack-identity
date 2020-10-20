using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.UserRoleController
{
    public class RemoveUserRoleModel
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}