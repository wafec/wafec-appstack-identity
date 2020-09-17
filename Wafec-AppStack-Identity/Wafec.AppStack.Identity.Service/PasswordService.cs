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
        public string GenerateHash(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed sha256Managed = new SHA256Managed();
            byte[] hash = sha256Managed.ComputeHash(bytes);
            return string.Join("", hash.Select(x => x.ToString("x2")));
        }

        public bool IsStrongEnought(string password)
        {
            return !string.IsNullOrEmpty(password) &&
                password.Length > 8 &&
                !password.ToUpper().Equals(password) &&
                password.Any(c => char.IsDigit(c)) &&
                password.Any(c => !char.IsLetterOrDigit(c));
        }
    }
}
