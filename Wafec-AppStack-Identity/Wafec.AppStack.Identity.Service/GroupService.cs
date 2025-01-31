﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class GroupService : BaseService, IGroupService
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
            return Repository.GetSet<Group>().Any(g => g.Deleted == false && g.Name.ToLower().Equals(name?.ToLower()));
        }

        public UserGroup AddUserGroup(long groupId, long userId)
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
            var group = Repository.GetSet<Group>().FirstOrDefault(g => g.Deleted == false && g.Id == id);
            if (group == null)
                throw new NotFoundException();
            else
                return group;
        }

        public GroupRole AddGroupRole(long groupId, long roleId)
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

        public void DeleteGroup(long id)
        {
            var group = FindGroup(id);
            group.Deleted = true;
            Repository.Update(group);
        }

        public void RemoveUserGroup(long groupId, long userId)
        {
            var userGroup = FindUserGroup(groupId, userId);
            userGroup.Deleted = true;
            Repository.Update(userGroup);
        }

        public void RemoveGroupRole(long groupId, long roleId)
        {
            var groupRole = FindGroupRole(groupId, roleId);
            groupRole.Deleted = true;
            Repository.Update(groupRole);
        }

        public UserGroup FindUserGroup(long groupId, long userId)
        {
            var userGroup = Repository.GetSet<UserGroup>().FirstOrDefault(ug => ug.Deleted == false && ug.GroupId == groupId && ug.UserId == userId);
            if (userGroup != null)
                return userGroup;
            else
                throw new NotFoundException();
        }

        public GroupRole FindGroupRole(long groupId, long roleId)
        {
            var groupRole = Repository.GetSet<GroupRole>().FirstOrDefault(gr => gr.Deleted == false && gr.GroupId == groupId && gr.RoleId == roleId);
            if (groupRole != null)
                return groupRole;
            else
                throw new NotFoundException();
        }
    }
}
