using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class ProjectGroup
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public long GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
