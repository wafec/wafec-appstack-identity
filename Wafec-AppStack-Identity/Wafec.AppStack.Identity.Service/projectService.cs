using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Service
{
    public class ProjectService : IProjectService
    {
        public ServiceContext ServiceContext { get; private set; }

        public ProjectService(ServiceContext serviceContext)
        {
            this.ServiceContext = serviceContext;
        }

        public Project CreateProject(string name, string description, User owner)
        {
            if (!ProjectExists(name))
            {
                Project project = new Project()
                {
                    Name = name,
                    Description = description,
                    Owner = owner
                };
                ServiceContext.ProjectSet.Add(project);
                return project;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public bool ProjectExists(string name)
        {
            return ServiceContext.ProjectSet.Any(p => p.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
