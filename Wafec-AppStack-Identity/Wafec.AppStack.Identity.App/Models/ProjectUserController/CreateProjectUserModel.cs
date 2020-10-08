using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.ProjectUserController
{
    public class CreateProjectUserModel
    {
        public long UserId { get; set; }
        public long ProjectId { get; set; }
    }
}