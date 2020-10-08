using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class GroupService : IGroupService
    {
        public IRepository Repository { get; private set; }
        public IUserService UserService { get; private set; }
        public IRoleService RoleService { get; private set; }

        public GroupService(IRepository repository, IUserService userService, IRoleService roleService)
        {
            Repository = repository;
            UserService = userService;
            RoleService = roleService;
        }

        public Group CreateGroup(string name)
        {
            if (!GroupExists(name))
            {
                Group group = new Group();
                group.Name = name;
                Repository.Add(group);
                return group;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public bool GroupExists(string name)
        {
            return Repository.GetSet<Group>().Any(g => g.Name.ToLower().Equals(name?.ToLower()));
        }

        public UserGroup AddUser(long groupId, long userId)
        {
            if (!Repository.GetSet<UserGroup>().Any(ug => ug.UserId == userId && ug.GroupId == groupId))
            {
                var user = UserService.FindUser(userId);
                var group = FindGroup(groupId);
                UserGroup userGroup = new UserGroup();
                userGroup.User = user;
                userGroup.Group = group;
                Repository.Add(userGroup);
                return userGroup;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public Group FindGroup(long id)
        {
            var group = Repository.GetSet<Group>().FirstOrDefault(g => g.Id == id);
            if (group == null)
                throw new NotFoundException();
            else
                return group;
        }

        public GroupRole AddRole(long groupId, long roleId)
        {
            if (!Repository.GetSet<GroupRole>().Any(gr => gr.GroupId == groupId && gr.RoleId == roleId))
            {
                var group = FindGroup(groupId);
                var role = RoleService.FindRole(roleId);
                var groupRole = new GroupRole();
                groupRole.Group = group;
                groupRole.Role = role;
                Repository.Add(groupRole);
                return groupRole;
            }
            else
            {
                throw new ConflictException();
            }
        }
    }
}
