using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class ProjectGroupRole
    {
        public long Id { get; set; }
        public long ProjectGroupId { get; set; }
        public virtual ProjectGroup ProjectGroup { get; set; }
        public long ProjectRoleId { get; set; }
        public virtual ProjectRole ProjectRole { get; set; }
    }
}
