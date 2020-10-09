using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Service
{
    public class PasswordService : IPasswordService
    {
        public string GenerateHash(string password, PasswordAlgorithms algorithm)
        {
            switch (algorithm)
            {
                case PasswordAlgorithms.SHA256:
                    return GenerateHashWithSHA256(password);
            }
            throw new ArgumentException();
        }

        private string GenerateHashWithSHA256(string password)
        {
            if (password == null)
                throw new ArgumentNullException();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed sha256Managed = new SHA256Managed();
            byte[] hash = sha256Managed.ComputeHash(bytes);
            return string.Join("", hash.Select(x => x.ToString("x2")));
        }

        private bool IsWeakEnough(string password)
        {
            return !string.IsNullOrEmpty(password) &&
                password.Length >= 8 &&
                password.Length <= 50;
        }

        private bool IsMediumEnough(string password)
        {
            return password.Where(c => char.IsLetter(c)).Count() >= 2 &&
                password.Any(c => char.IsLetter(c) && char.IsUpper(c));
        }

        private bool IsStrongEnough(string password)
        {
            return password.Any(c => char.IsDigit(c)) &&
                password.Any(c => !char.IsLetterOrDigit(c));
        }

        public bool IsStrongEnough(string password, PasswordLevels level)
        {
            if (password == null)
                throw new ArgumentNullException();

            var levelBits = (level == PasswordLevels.WEAK ? 1 : 0) |
                (level == PasswordLevels.MEDIUM ? 3 : 0) |
                (level == PasswordLevels.STRONG ? 7 : 0);

            return
                !password.Any(c => char.IsWhiteSpace(c)) &&
                ((levelBits & 1) == 0 || IsWeakEnough(password)) &&
                ((levelBits & 2) == 0 || IsMediumEnough(password)) &&
                ((levelBits & 4) == 0 || IsStrongEnough(password));
        }
    }
}
