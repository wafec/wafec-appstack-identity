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
            roleList.Add(new Role() { Name = "Role1", Description = "RoleDescription1" });
            roleList.Add(new Role() { Name = "Role2", Description = "RoleDescription2" });
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
    }
}
