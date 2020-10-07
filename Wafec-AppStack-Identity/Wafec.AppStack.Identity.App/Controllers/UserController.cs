using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.UserController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class UserController : ApiController
    {
        public IRepository Repository { get; private set; }
        public IUserService UserService { get; private set; }

        public UserController(IRepository repository, IUserService userService)
        {
            Repository = repository;
            UserService = userService;
        }

        public IHttpActionResult Post([FromBody] CreateUserModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var user = UserService.CreateUser(model.Username, model.Password);
                t.Commit();
                return Json(user);
            }
        }
    }
}
