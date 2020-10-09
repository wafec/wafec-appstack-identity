using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Deleted { get; set; }
        public long PasswordAlgorithmId { get; set; }
        public virtual PasswordAlgorithm PasswordAlgorithm { get; set; }
        public long PasswordLevelId { get; set; }
        public virtual PasswordLevel PasswordLevel { get; set; }
    }
}
