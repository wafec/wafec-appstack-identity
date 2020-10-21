using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Shared.Lang;

namespace Wafec.AppStack.Identity.Service
{
    public class ProjectService : IProjectService
    {
        public IRepository Repository { get; private set; }
        public IUserService UserService { get; private set; }
        public IRoleService RoleService { get; private set; }
        public IGroupService GroupService { get; private set; }

        public ProjectService(IRepository repository, IUserService userService, IRoleService roleService, IGroupService groupService)
        {
            this.Repository = repository;
            this.UserService = userService;
            this.RoleService = roleService;
            this.GroupService = groupService;
        }

        public Project CreateProject(string name, string description, long ownerId)
        {
            if (!ExistsProject(name))
            {
                User owner = UserService.FindUser(ownerId);
                Project project = new Project()
                {
                    Name = name,
                    Description = description,
                    Owner = owner
                };
                Repository.Add(project);
                return project;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public bool ExistsProject(string name)
        {
            return !Utility.TrueIfThrows<NotFoundException>(() => {
                FindProject(name);
            });
        }

        public Project FindProject(string name)
        {
            var project = Repository.GetSet<Project>().FirstOrDefault(p => p.Deleted == false && p.Name.ToLower().Equals(name.ToLower()));
            if (project != null)
                return project;
            else
                throw new NotFoundException();
        }

        public ProjectUser AddProjectUser(long projectId, long userId)
        {
            if (!Repository.GetSet<ProjectUser>().Any(pu => pu.ProjectId == projectId && pu.UserId == userId))
            {
                var user = UserService.FindUser(userId);
                var project = FindProject(projectId);
                var projectUser = new ProjectUser();
                projectUser.Project = project;
                projectUser.User = user;
                Repository.Add(projectUser);
                return projectUser;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public Project FindProject(long id)
        {
            var project = Repository.GetSet<Project>().FirstOrDefault(p => p.Deleted == false && p.Id == id);
            if (project != null)
                return project;
            else
                throw new NotFoundException();
        }

        public ProjectRole AddRole(long projectId, long roleId)
        {
            if (!Repository.GetSet<ProjectRole>().Any(pr => pr.ProjectId == projectId && pr.RoleId == roleId))
            {
                var role = RoleService.FindRole(roleId);
                var project = FindProject(projectId);
                var projectRole = new ProjectRole();
                projectRole.Project = project;
                projectRole.Role = role;
                Repository.Add(projectRole);
                return projectRole;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public ProjectGroup AddGroup(long projectId, long groupId)
        {
            if (!Repository.GetSet<ProjectGroup>().Any(pg => pg.GroupId == groupId && pg.ProjectId == projectId))
            {
                var group = GroupService.FindGroup(groupId);
                var project = FindProject(projectId);
                var projectGroup = new ProjectGroup();
                projectGroup.Project = project;
                projectGroup.Group = group;
                Repository.Add(projectGroup);
                return projectGroup;
            }
            else
            {
                throw new ConflictException();
            }
        }

        private ProjectUserRole AddUserRole(ProjectUser projectUser, ProjectRole projectRole)
        {
            var projectUserRole = new ProjectUserRole();
            projectUserRole.ProjectUser = projectUser;
            projectUserRole.ProjectRole = projectRole;
            Repository.Add(projectUserRole);
            return projectUserRole;
        }

        public ProjectUserRole AddUserRole(long projectUserId, long projectRoleId)
        {
            if (!Repository.GetSet<ProjectUserRole>().Any(pur => pur.ProjectUserId == projectUserId && pur.ProjectRoleId == projectRoleId))
            {
                var projectUser = FindProjectUser(projectUserId);
                var projectRole = FindProjectRole(projectRoleId);
                return AddUserRole(projectUser, projectRole);
            }
            else
            {
                throw new ConflictException();
            }
        }

        public ProjectUser FindProjectUser(long id)
        {
            var projectUser = Repository.GetSet<ProjectUser>().FirstOrDefault(pu => pu.Deleted == false && pu.Id == id);
            if (projectUser != null)
                return projectUser;
            else
                throw new NotFoundException();
        }

        public ProjectRole FindProjectRole(long id)
        {
            var projectRole = Repository.GetSet<ProjectRole>().FirstOrDefault(pr => pr.Deleted == false && pr.Id == id);
            if (projectRole != null)
                return projectRole;
            else
                throw new NotFoundException();
        }

        private ProjectGroupRole AddGroupRole(ProjectGroup projectGroup, ProjectRole projectRole)
        {
            var projectGroupRole = new ProjectGroupRole();
            projectGroupRole.ProjectGroup = projectGroup;
            projectGroupRole.ProjectRole = projectRole;
            Repository.Add(projectGroupRole);
            return projectGroupRole;
        }

        public ProjectGroupRole AddGroupRole(long projectGroupId, long projectRoleId)
        {
            if (!Repository.GetSet<ProjectGroupRole>().Any(pgr => pgr.ProjectGroupId == projectGroupId && pgr.ProjectRoleId == projectRoleId))
            {
                var projectGroup = FindProjectGroup(projectGroupId);
                var projectRole = FindProjectRole(projectRoleId);
                return AddGroupRole(projectGroup, projectRole);
            }
            else
            {
                throw new ConflictException();
            }
        }

        public ProjectGroup FindProjectGroup(long id)
        {
            var projectGroup = Repository.GetSet<ProjectGroup>().FirstOrDefault(pg => pg.Deleted == false && pg.Id == id);
            if (projectGroup != null)
                return projectGroup;
            else
                throw new NotFoundException();
        }

        public Project UpdateProject(long id, string name, string description)
        {
            var project = FindProject(id);
            project.Name = name;
            project.Description = description;
            Repository.Update(project);
            return project;
        }

        public void DeleteProject(long id)
        {
            var project = FindProject(id);
            project.Deleted = true;
            Repository.Update(project);
        }

        public void RemoveRole(long projectId, long roleId)
        {
            var projectRole = FindProjectRole(projectId, roleId);
            projectRole.Deleted = true;
            Repository.Update(projectRole);
        }

        public void RemoveGroup(long projectId, long groupId)
        {
            var projectGroup = FindProjectGroup(projectId, groupId);
            projectGroup.Deleted = true;
            Repository.Update(projectGroup);
        }

        public void RemoveUserRole(long projectId, long userId, long roleId)
        {
            var projectUserRole = FindProjectUserRole(projectId, userId, roleId);
            projectUserRole.Deleted = true;
            Repository.Update(projectUserRole);
        }

        public void RemoveGroupRole(long projectId, long groupId, long roleId)
        {
            var projectGroupRole = FindProjectGroupRole(projectId, groupId, roleId);
            projectGroupRole.Deleted = true;
            Repository.Update(projectGroupRole);
        }

        public void RemoveProjectUser(long projectId, long userId)
        {
            var projectUser = FindProjectUser(projectId, userId);
            projectUser.Deleted = true;
            Repository.Update(projectUser);
        }

        public ProjectUser FindProjectUser(long projectId, long userId)
        {
            var projectUser = Repository.GetSet<ProjectUser>().FirstOrDefault(pu => pu.Deleted == false && pu.ProjectId == projectId && pu.UserId == userId);
            if (projectUser != null)
                return projectUser;
            else
                throw new NotFoundException();
        }

        public ProjectGroup FindProjectGroup(long projectId, long groupId)
        {
            var projectGroup = Repository.GetSet<ProjectGroup>().FirstOrDefault(pg => pg.Deleted == false && pg.ProjectId == projectId && pg.GroupId == groupId);
            if (projectGroup != null)
                return projectGroup;
            else
                throw new NotFoundException();
        }

        public ProjectRole FindProjectRole(long projectId, long roleId)
        {
            var projectRole = Repository.GetSet<ProjectRole>().FirstOrDefault(pr => pr.Deleted == false && pr.ProjectId == projectId && pr.RoleId == roleId);
            if (projectRole != null)
                return projectRole;
            else
                throw new NotFoundException();
        }

        public ProjectGroupRole FindProjectGroupRole(long projectId, long groupId, long roleId)
        {
            var projectGroupRole = Repository.GetSet<ProjectGroupRole>()
                .FirstOrDefault(pgr => pgr.ProjectGroup.GroupId == groupId && pgr.ProjectGroup.ProjectId == projectId &&
                                       pgr.ProjectRole.RoleId == roleId && pgr.ProjectRole.ProjectId == projectId &&
                                       pgr.Deleted == false);
            if (projectGroupRole != null)
                return projectGroupRole;
            else
                throw new NotFoundException();
        }

        public ProjectUserRole FindProjectUserRole(long projectId, long userId, long roleId)
        {
            var projectUserRole = Repository.GetSet<ProjectUserRole>()
                .FirstOrDefault(pur => pur.ProjectUser.UserId == userId && pur.ProjectUser.ProjectId == projectId &&
                                       pur.ProjectRole.RoleId == roleId && pur.ProjectRole.ProjectId == projectId &&
                                       pur.Deleted == false);
            if (projectUserRole != null)
                return projectUserRole;
            else
                throw new NotFoundException();
        }

        public ProjectUserRole AddProjectUserRole(long projectId, long userId, long roleId)
        {
            if (!ExistsProjectUserRole(projectId, userId, roleId))
            {
                var projectUser = FindProjectUser(projectId, userId);
                var projectRole = FindProjectRole(projectId, roleId);
                return AddUserRole(projectUser, projectRole);
            }
            else
            {
                throw new ConflictException();
            }
        }

        public ProjectGroupRole AddProjectGroupRole(long projectId, long groupId, long roleId)
        {
            if (!ExistsProjectGroupRole(projectId, groupId, roleId))
            {
                var projectGroup = FindProjectGroup(projectId, groupId);
                var projectRole = FindProjectRole(projectId, roleId);
                return AddGroupRole(projectGroup, projectRole);
            }
            else
            {
                throw new ConflictException();
            }
        }

        public bool ExistsProjectUserRole(long projectId, long userId, long roleId)
        {
            return !Utility.TrueIfThrows<NotFoundException>(() => 
            {
                FindProjectUserRole(projectId, userId, roleId);
            });
        }

        public bool ExistsProjectGroupRole(long projectId, long groupId, long roleId)
        {
            return !Utility.TrueIfThrows<NotFoundException>(() =>
            {
                FindProjectGroupRole(projectId, groupId, roleId);
            });
        }
    }
}
