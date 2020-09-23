using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class UserService : IUserService
    {
        public IRepository Repository { get; private set; }
        public IPasswordService PasswordService { get; private set; }

        public UserService(IRepository repository,
                           IPasswordService passwordService)
        {
            Repository = repository;
            PasswordService = passwordService;
        }

        public User CreateUser(string name, string password)
        {
            if (!UserExists(name))
            {
                if (!PasswordService.IsStrongEnough(password))
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
                    Repository.Add(user);
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
            return Repository.GetSet<User>().Any(u => u.Name.ToLower().Equals(name.ToLower()));
        }

        public bool UserExists(string name, string password)
        {
            return Repository.GetSet<User>().Any(u => u.Name.ToLower().Equals(name.ToLower()) && u.Password.Equals(password));
        }
    }
}
