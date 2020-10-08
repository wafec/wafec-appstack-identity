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
    public class UserServiceTests
    {
        public IEnumerable<User> UserSet { get; private set; }
        public Mock<ITransaction> MockTransaction { get; private set; }
        public Mock<IRepository> MockRepository { get; private set; }
        public Mock<IPasswordService> MockPasswordService { get; private set; }
        
        public IPasswordService PasswordService
        {
            get
            {
                return MockPasswordService.Object;
            }
        }

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
                return new UserService(Repository, PasswordService, null);
            }
        }

        [TestInitialize]
        public void SetUp()
        {
            var userList = new List<User>();
            userList.Add(new User() { Name = "User1", Password = "User1PasswordTest", Id = 1 });
            userList.Add(new User() { Name = "User2", Password = "User2PasswordTest", Id = 2 });
            UserSet = userList;
            MockTransaction = new Mock<ITransaction>();
            MockRepository = new Mock<IRepository>();
            MockRepository.Setup(repository => repository.GetSet<User>()).Returns(() => UserSet);
            MockRepository.Setup(repository => repository.BeginTransaction()).Returns(() => Transaction);
            MockPasswordService = new Mock<IPasswordService>();
            MockPasswordService.Setup(passwordService => passwordService.IsStrongEnough(It.IsAny<string>())).Returns(true);
            MockPasswordService.Setup(passwordService => passwordService.GenerateHash(It.IsAny<string>())).Returns("UserPasswordTest");
        }

        [TestMethod]
        public void TestCreateExistentUser()
        {
            Assert.ThrowsException<ConflictException>(() =>
            {
                UserService.CreateUser("User1", "Any");
            });
        }

        [TestMethod]
        public void TestCreateWithNotStringPassword()
        {
            MockPasswordService.Setup(passwordService => passwordService.IsStrongEnough(It.IsAny<string>())).Returns(false);
            Assert.ThrowsException<WeakPasswordException>(() =>
            {
                UserService.CreateUser("User3", "Any");
            });
        }

        [TestMethod]
        public void TestCreateUser()
        {
            var user = UserService.CreateUser("User3", "Any");
            Assert.IsNotNull(user);
            Assert.AreEqual("User3", user.Name);
            Assert.AreEqual("UserPasswordTest", user.Password);
        }

        [TestMethod]
        public void TestArgumentNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                UserService.CreateUser(null, "Any");
            });
            Assert.ThrowsException<ArgumentNullException>(() => 
            {
                UserService.CreateUser("User3", null);
            });
        }

        [TestMethod]
        public void TestUserExists()
        {
            var result = UserService.UserExists("User1");
            Assert.IsTrue(result);
            result = UserService.UserExists("USER1");
            Assert.IsTrue(result);
            result = UserService.UserExists("User3");
            Assert.IsFalse(result);
        }
    }
}
