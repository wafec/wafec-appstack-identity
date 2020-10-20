using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.ProjectGroupController
{
    public class RemoveProjectGroupModel
    {
        public long ProjectId { get; set; }
        public long GroupId { get; set; }
    }
}