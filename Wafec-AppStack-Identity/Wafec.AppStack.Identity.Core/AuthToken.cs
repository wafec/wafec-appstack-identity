using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class AuthToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public string Token { get; set; }
        public string Refresh { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }
    }
}
