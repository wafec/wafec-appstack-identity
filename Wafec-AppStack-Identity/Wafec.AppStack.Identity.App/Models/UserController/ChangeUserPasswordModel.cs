using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.UserController
{
    public class ChangeUserPasswordModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}