using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Core.Database
{
    public interface IRepository
    {
        IEnumerable<T> GetSet<T>() where T : class;
        T Add<T>(T obj) where T : class;
        ITransaction BeginTransaction();
    }
}
