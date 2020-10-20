using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class UserGroup
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long GroupId { get; set; }
        public virtual Group Group { get; set; }
        public bool Deleted { get; set; }
    }
}
