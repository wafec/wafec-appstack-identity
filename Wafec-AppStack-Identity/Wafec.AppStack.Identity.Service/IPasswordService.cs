using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Service
{
    public interface IPasswordService
    {
        string GenerateHash(string password, PasswordAlgorithms algorithm);
        bool IsStrongEnough(string password, PasswordLevels level);
    }
}
