using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Configuration.Database;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.Configuration.IoC
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository>().To<ServiceContext>().InRequestScope();
            Bind<IPasswordService>().To<PasswordService>();
            Bind<IUserService>().To<UserService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IProjectService>().To<ProjectService>();
            Bind<IGroupService>().To<GroupService>();
        }
    }
}
