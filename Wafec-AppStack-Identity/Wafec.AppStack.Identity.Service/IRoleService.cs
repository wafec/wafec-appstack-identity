using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Service
{
    public interface IRoleService
    {
        Role CreateRole(string name, string description);
        Role FindRole(long id);
        bool RoleExists(string name);
    }
}
