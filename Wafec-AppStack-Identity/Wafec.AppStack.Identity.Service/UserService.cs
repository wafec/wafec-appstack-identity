using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Service
{
    public class UserService : IUserService
    {
        public ServiceContext ServiceContext { get; private set; }
        public IPasswordService PasswordService { get; private set; }

        public UserService(ServiceContext serviceContext,
                           IPasswordService passwordService)
        {
            ServiceContext = serviceContext;
            PasswordService = passwordService;
        }

        public User CreateUser(string name, string password)
        {
            if (!UserExists(name))
            {
                if (!PasswordService.IsStrongEnought(password))
                {
                    throw new WeakPasswordException();
                }
                else
                {
                    User user = new User()
                    {
                        Name = name,
                        Password = PasswordService.GenerateHash(password)
                    };
                    return user;
                }
            }
            else
            {
                throw new ConflictException();
            }
        }

        public bool UserExists(string name)
        {
            return ServiceContext.UserSet.Any(u => u.Name.ToLower().Equals(name.ToLower()));
        }

        public bool UserExists(string name, string password)
        {
            return ServiceContext.UserSet.Any(u => u.Name.ToLower().Equals(name.ToLower()) && u.Password.Equals(password));
        }
    }
}
