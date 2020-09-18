using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core.Database
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
