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
    public class RoleServiceTests
    {
        public Mock<ITransaction> MockTransaction { get; private set; }
        public Mock<IRepository> MockRepository { get; private set; }
        public IEnumerable<Role> RoleSet { get; private set; }

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

        public IRoleService RoleService
        {
            get
            {
                return new RoleService(Repository);
            }
        }

        [TestInitialize]
        public void SetUp()
        {
            var roleList = new List<Role>();
            roleList.Add(new Role() { Name = "Role1", Description = "RoleDescription1", Id = 1 });
            roleList.Add(new Role() { Name = "Role2", Description = "RoleDescription2", Id = 2 });
            RoleSet = roleList;
            MockTransaction = new Mock<ITransaction>();
            MockRepository = new Mock<IRepository>();
            MockRepository.Setup(repository => repository.GetSet<Role>()).Returns(() => RoleSet);
            MockRepository.Setup(repository => repository.BeginTransaction()).Returns(() => Transaction);
        }

        [TestMethod]
        public void TestCreateExistentRole()
        {
            Assert.ThrowsException<ConflictException>(() =>
            {
                RoleService.CreateRole("Role1", "Any");
            });
        }

        [TestMethod]
        public void TestCreateRole()
        {
            var role = RoleService.CreateRole("Role3", "Any");
            Assert.AreEqual("Role3", role.Name);
            Assert.AreEqual("Any", role.Description);
        }

        [TestMethod]
        public void TestRoleExists()
        {
            var result = RoleService.RoleExists("Role1");
            Assert.IsTrue(result);
            result = RoleService.RoleExists("ROLE1");
            Assert.IsTrue(result);
            result = RoleService.RoleExists("Role3");
            Assert.IsFalse(result);
            result = RoleService.RoleExists(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestFindRoleWhenItDoesNotExist()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                RoleService.FindRole(3);
            });
        }

        [TestMethod]
        public void TestFindRoleWhenItExists()
        {
            var role = RoleService.FindRole(1);
            Assert.IsNotNull(role);
            Assert.AreEqual(1, role.Id);
            Assert.AreEqual("Role1", role.Name);
        }
    }
}
