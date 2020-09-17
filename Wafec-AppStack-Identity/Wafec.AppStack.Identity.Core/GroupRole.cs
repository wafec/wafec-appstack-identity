using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class GroupRole
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public virtual Group Group { get; set; }
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
