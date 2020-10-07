using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class GroupService : IGroupService
    {
        public IRepository Repository { get; private set; }

        public GroupService(IRepository repository)
        {
            Repository = repository;
        }

        public Group CreateGroup(string name)
        {
            if (!Repository.GetSet<Group>().Any(g => g.Name.ToLower().Equals(name?.ToLower())))
            {
                Group group = new Group();
                group.Name = name;
                Repository.Add(group);
                return group;
            }
            else
            {
                throw new ConflictException();
            }
        }
    }
}
