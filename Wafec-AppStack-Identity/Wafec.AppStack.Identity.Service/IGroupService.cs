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
        UserGroup AddUserGroup(long groupId, long userId);
        Group FindGroup(long id);
        GroupRole AddGroupRole(long groupId, long roleId);
        void DeleteGroup(long id);
    }
}
