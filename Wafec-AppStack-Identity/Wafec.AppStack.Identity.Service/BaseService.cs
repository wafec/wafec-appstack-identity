using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Service
{
    public abstract class BaseService
    {
        protected void ThrowsIfNull(object obj)
        {
            if (obj == null)
                throw new InvalidDataException();
        }

        protected void ThrowsIfEmpty(string obj)
        {
            ThrowsIfNull(obj);
            if (string.IsNullOrEmpty(obj))
                throw new InvalidDataException();
        }

        protected void ThrowsIfEmpty(params string[] objArgs)
        {
            foreach (var obj in objArgs)
                ThrowsIfEmpty(obj);
        }
    }
}
