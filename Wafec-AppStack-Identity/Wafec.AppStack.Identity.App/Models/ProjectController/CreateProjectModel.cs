using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.ProjectController
{
    public class CreateProjectModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long OwnerId { get; set; }
    }
}