using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class RoleService : IRoleService
    {
        public IRepository Repository { get; private set; }

        public RoleService(IRepository repository)
        {
            Repository = repository;
        }

        public Role CreateRole(string name, string description)
        {
            if (!Repository.GetSet<Role>().Any(r => r.Name.ToLower().Equals(name?.ToLower())))
            {
                Role role = new Role();
                role.Name = name;
                role.Description = description;
                Repository.Add(role);
                return role;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public Role FindRole(long id)
        {
            Role result = Repository.GetSet<Role>().FirstOrDefault(r => r.Id == id);
            if (result != null)
                return result;
            else
                throw new NotFoundException();
        }
    }
}
