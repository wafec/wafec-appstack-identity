using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class ProjectRole
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
        public bool Deleted { get; set; }
    }
}
