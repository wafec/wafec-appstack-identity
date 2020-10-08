using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.UserGroupController
{
    public class CreateUserGroupModel
    {
        public long GroupId { get; set; }
        public long UserId { get; set; }
    }
}