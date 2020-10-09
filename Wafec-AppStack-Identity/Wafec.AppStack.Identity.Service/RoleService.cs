using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class RoleService : BaseService, IRoleService
    {
        public IRepository Repository { get; private set; }

        public RoleService(IRepository repository)
        {
            Repository = repository;
        }

        public Role CreateRole(string name, string description)
        {
            ThrowsIfEmpty(name);

            if (!RoleExists(name))
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

        public bool RoleExists(string name)
        {
            ThrowsIfEmpty(name);

            return Repository.GetSet<Role>().Any(r => r.Deleted == false && r.Name.ToLower().Equals(name?.ToLower()));
        }

        public Role FindRole(long id)
        {
            Role result = Repository.GetSet<Role>().FirstOrDefault(r => r.Deleted == false && r.Id == id);
            if (result != null)
                return result;
            else
                throw new NotFoundException();
        }

        public Role UpdateRole(long id, string name, string description)
        {
            ThrowsIfEmpty(name);

            if (!Repository.GetSet<Role>().Any(r => r.Deleted == false && r.Id != id && r.Name.ToLower().Equals(name?.ToLower())))
            {
                var role = FindRole(id);
                role.Name = name;
                role.Description = description;
                role = Repository.Update(role);
                return role;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public void DeleteRole(long id)
        {
            var role = FindRole(id);
            role.Deleted = true;
            role = Repository.Update(role);
        }
    }
}
