using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.ServiceTests
{
    [TestClass]
    public class PasswordServiceTests
    {
        public IPasswordService PasswordService { get; private set; }

        [TestInitialize]
        public void SetUp()
        {
            PasswordService = new PasswordService();
        }

        [TestMethod]
        public void TestIsStrongEnoughWhenEmpty()
        {
            var result = PasswordService.IsStrongEnough("", PasswordLevels.STRONG);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsStrongEnoughWhenNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
            PasswordService.IsStrongEnough(null, PasswordLevels.STRONG);
            });
        }

        [TestMethod]
        public void TestIsStrongEnoughWhenShort()
        {
            var result = PasswordService.IsStrongEnough("1234567", PasswordLevels.STRONG);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsStrongEnoughWhenBreakingRules()
        {
            var result = PasswordService.IsStrongEnough("12345678", PasswordLevels.STRONG);
            Assert.IsFalse(result);
            result = PasswordService.IsStrongEnough("AAAAAAAA", PasswordLevels.STRONG);
            Assert.IsFalse(result);
            result = PasswordService.IsStrongEnough("Aaaaaaaa", PasswordLevels.STRONG);
            Assert.IsFalse(result);
            result = PasswordService.IsStrongEnough("A1234567", PasswordLevels.STRONG);
            Assert.IsFalse(result);
            result = PasswordService.IsStrongEnough("A 123@a456", PasswordLevels.STRONG);
            Assert.IsFalse(result);
            result = PasswordService.IsStrongEnough("A123456a", PasswordLevels.STRONG);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsStrongEnough()
        {
            var result = PasswordService.IsStrongEnough("%a123456A", PasswordLevels.STRONG);
            Assert.IsTrue(result);
        }

        private string GenerateValidPasswordRandom(int size = 8)
        {
            char[] buffer = new char[] { '%', 'a', 'A', '1' };
            string password = "";
            for (int i = 0; i < size; i++)
                password += buffer[i % buffer.Length];
            return password;
        }

        [TestMethod]
        public void TestIsStrongEnoughWhenBiggerButValid()
        {
            var password = GenerateValidPasswordRandom(51);
            var result = PasswordService.IsStrongEnough(password, PasswordLevels.STRONG);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsStrongEnoughWhenAlmostBiggerButValid()
        {
            var password = GenerateValidPasswordRandom(50);
            var result = PasswordService.IsStrongEnough(password, PasswordLevels.STRONG);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGenerateHashWhenNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                PasswordService.GenerateHash(null, PasswordAlgorithms.SHA256);
            });
        }
    }
}
