using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class ProjectUserRole
    {
        public long Id { get; set; }
        public long ProjectUserId { get; set; }
        public virtual ProjectUser ProjectUser { get; set; }
        public long ProjectRoleId { get; set; }
        public virtual ProjectRole ProjectRole { get; set; }
        public bool Deleted { get; set; }
    }
}
