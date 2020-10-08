using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Service
{
    public interface IGroupService
    {
        Group CreateGroup(string name);
        bool GroupExists(string name);
        UserGroup AddUser(long groupId, long userId);
        Group FindGroup(long id);
        GroupRole AddRole(long groupId, long roleId);
    }
}
