using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Service
{
    public interface IProjectService
    {
        Project CreateProject(string name, string description, long ownerId);
        bool ProjectExists(string name);
        ProjectUser AddProjectUser(long projectId, long userId);
        Project FindProject(long id);
        ProjectRole AddRole(long projectId, long roleId);
        ProjectGroup AddGroup(long projectId, long groupId);
        ProjectUserRole AddUserRole(long projectUserId, long projectRoleId);
        ProjectUser FindProjectUser(long id);
        ProjectRole FindProjectRole(long id);
        ProjectGroupRole AddGroupRole(long projectGroupId, long projectRoleId);
        ProjectGroup FindProjectGroup(long id);
    }
}
