using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Service
{
    public interface IPasswordService
    {
        String GenerateHash(string password);
        bool IsStrongEnought(string password);
    }
}
