using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

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
            if (!ProjectExists(name))
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

        public bool ProjectExists(string name)
        {
            return Repository.GetSet<Project>().Any(p => p.Name.ToLower().Equals(name.ToLower()));
        }

        public ProjectUser AddUser(long projectId, long userId)
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
            var project = Repository.GetSet<Project>().FirstOrDefault(p => p.Id == id);
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

        public ProjectUserRole AddUserRole(long projectUserId, long projectRoleId)
        {
            if (!Repository.GetSet<ProjectUserRole>().Any(pur => pur.ProjectUserId == projectUserId && pur.ProjectRoleId == projectRoleId))
            {
                var projectUser = FindProjectUser(projectUserId);
                var projectRole = FindProjectRole(projectRoleId);
                var projectUserRole = new ProjectUserRole();
                projectUserRole.ProjectUser = projectUser;
                projectUserRole.ProjectRole = projectRole;
                Repository.Add(projectUserRole);
                return projectUserRole;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public ProjectUser FindProjectUser(long id)
        {
            var projectUser = Repository.GetSet<ProjectUser>().FirstOrDefault(pu => pu.Id == id);
            if (projectUser != null)
                return projectUser;
            else
                throw new NotFoundException();
        }

        public ProjectRole FindProjectRole(long id)
        {
            var projectRole = Repository.GetSet<ProjectRole>().FirstOrDefault(pr => pr.Id == id);
            if (projectRole != null)
                return projectRole;
            else
                throw new NotFoundException();
        }

        public ProjectGroupRole AddGroupRole(long projectGroupId, long projectRoleId)
        {
            if (!Repository.GetSet<ProjectGroupRole>().Any(pgr => pgr.ProjectGroupId == projectGroupId && pgr.ProjectRoleId == projectRoleId))
            {
                var projectGroup = FindProjectGroup(projectGroupId);
                var projectRole = FindProjectRole(projectRoleId);
                var projectGroupRole = new ProjectGroupRole();
                projectGroupRole.ProjectGroup = projectGroup;
                projectGroupRole.ProjectRole = projectRole;
                Repository.Add(projectGroupRole);
                return projectGroupRole;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public ProjectGroup FindProjectGroup(long id)
        {
            var projectGroup = Repository.GetSet<ProjectGroup>().FirstOrDefault(pg => pg.Id == id);
            if (projectGroup != null)
                return projectGroup;
            else
                throw new NotFoundException();
        }
    }
}
