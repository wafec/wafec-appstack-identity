using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Service
{
    public interface IProjectService
    {
        Project CreateProject(string name, string description, User owner);
        bool ProjectExists(string name);
    }
}
