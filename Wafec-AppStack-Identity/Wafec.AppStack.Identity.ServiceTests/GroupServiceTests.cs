using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.ServiceTests
{
    [TestClass]
    public class GroupServiceTests
    {
        public Mock<IRepository> MockRepository { get; private set; }
        public Mock<ITransaction> MockTransaction { get; private set; }
        public Mock<IUserService> MockUserService { get; private set; }
        public Mock<IRoleService> MockRoleService { get; private set; }
        public IEnumerable<User> UserSet { get; private set; }
        public IEnumerable<Role> RoleSet { get; private set; }
        public IEnumerable<Group> GroupSet { get; private set; }

        public ITransaction Transaction
        {
            get
            {
                return MockTransaction.Object;
            }
        }

        public IRepository Repository
        {
            get
            {
                return MockRepository.Object;
            }
        }

        public IUserService UserService
        {
            get
            {
                return MockUserService.Object;
            }
        }

        public IRoleService RoleService
        {
            get
            {
                return MockRoleService.Object;
            }
        }

        public IGroupService GroupService
        {
            get
            {
                return new GroupService(Repository, UserService, RoleService);
            }
        }

        [TestInitialize]
        public void SetUp()
        {
            var userList = new List<User>();
            userList.Add(new User() { Id = 1, Name = "User1", Password = "User1Passwd" });
            userList.Add(new User() { Id = 2, Name = "User2", Password = "User2Passwd" });
            UserSet = userList;
            var roleList = new List<Role>();
            roleList.Add(new Role() { Id = 1, Name = "Role1", Description = "Any1" });
            roleList.Add(new Role() { Id = 2, Name = "Role2", Description = "Any2" });
            RoleSet = roleList;
            var groupList = new List<Group>();
            groupList.Add(new Group() { Id = 1, Name = "Group1" });
            groupList.Add(new Group() { Id = 2, Name = "Group2" });
            GroupSet = groupList;
            MockTransaction = new Mock<ITransaction>();
            MockRepository = new Mock<IRepository>();
            MockRepository.Setup(repo => repo.BeginTransaction()).Returns(() => Transaction);
            MockUserService = new Mock<IUserService>();
            MockUserService.Setup(u => u.FindUser(1)).Returns(() => UserSet.First(u => u.Id == 1));
            MockUserService.Setup(u => u.FindUser(2)).Returns(() => UserSet.First(u => u.Id == 2));
            MockRoleService = new Mock<IRoleService>();
            MockRoleService.Setup(r => r.FindRole(1)).Returns(() => RoleSet.First(r => r.Id == 1));
            MockRoleService.Setup(r => r.FindRole(2)).Returns(() => RoleSet.First(r => r.Id == 2));
            MockRepository.Setup(repo => repo.GetSet<Group>()).Returns(() => GroupSet);
        }

        [TestMethod]
        public void TestCreateGroupWhenConflicting()
        {
            Assert.ThrowsException<ConflictException>(() =>
            {
                GroupService.CreateGroup("Group2");
            });
            Assert.ThrowsException<ConflictException>(() => 
            {
                GroupService.CreateGroup("GROUP2");
            });
        }

        [TestMethod]
        public void TestCreateGroup()
        {
            var group = GroupService.CreateGroup("Group3");
            Assert.AreEqual("Group3", group.Name);
        }

        [TestMethod]
        public void TestFindGroupWhenItDoesNotExist()
        {
            Assert.ThrowsException<NotFoundException>(() => 
            {
                GroupService.FindGroup(3);
            });
        }

        [TestMethod]
        public void TestFindGroup()
        {
            var group = GroupService.FindGroup(1);
            Assert.AreEqual(1, group.Id);
            Assert.AreEqual("Group1", group.Name);
        }

        [TestMethod]
        public void TestGroupExists()
        {
            var result = GroupService.GroupExists("Group1");
            Assert.IsTrue(result);
            result = GroupService.GroupExists("GROUP1");
            Assert.IsTrue(result);
            result = GroupService.GroupExists("Group3");
            Assert.IsFalse(result);
        }
    }
}
