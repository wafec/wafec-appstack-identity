using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class ProjectUser
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public bool Deleted { get; set; }
    }
}
