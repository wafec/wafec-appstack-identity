using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wafec.AppStack.Identity.App.Models.TokenController
{
    public class CreateTokenModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}