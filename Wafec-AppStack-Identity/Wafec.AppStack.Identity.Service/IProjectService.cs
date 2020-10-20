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
        bool ExistsProject(string name);
        ProjectUser AddProjectUser(long projectId, long userId);
        Project FindProject(long id);
        Project FindProject(string name);
        ProjectRole AddRole(long projectId, long roleId);
        ProjectGroup AddGroup(long projectId, long groupId);
        ProjectUserRole AddUserRole(long projectUserId, long projectRoleId);
        ProjectUserRole AddProjectUserRole(long projectId, long userId, long roleId);
        bool ExistsProjectUserRole(long projectId, long userId, long roleId);
        ProjectUser FindProjectUser(long id);
        ProjectRole FindProjectRole(long id);
        ProjectGroupRole AddGroupRole(long projectGroupId, long projectRoleId);
        ProjectGroupRole AddProjectGroupRole(long projectId, long groupId, long roleId);
        bool ExistsProjectGroupRole(long projectId, long groupId, long roleId);
        ProjectGroup FindProjectGroup(long id);
        Project UpdateProject(long id, string name, string description);
        void DeleteProject(long id);
        void RemoveRole(long projectId, long roleId);
        void RemoveGroup(long projectId, long groupId);
        void RemoveUserRole(long projectId, long userId, long roleId);
        void RemoveGroupRole(long projectId, long groupId, long roleId);
        void RemoveProjectUser(long projectId, long userId);
        ProjectUser FindProjectUser(long projectId, long userId);
        ProjectGroup FindProjectGroup(long projectId, long groupId);
        ProjectRole FindProjectRole(long projectId, long roleId);
        ProjectGroupRole FindProjectGroupRole(long projectId, long groupId, long roleId);
        ProjectUserRole FindProjectUserRole(long projectId, long userId, long roleId);
    }
}
