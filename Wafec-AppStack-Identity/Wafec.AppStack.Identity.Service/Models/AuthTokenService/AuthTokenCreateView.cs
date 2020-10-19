using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafec.AppStack.Identity.Service.Models.AuthTokenService
{
    public class AuthTokenCreateView
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public string Refresh { get; set; }
        public DateTime Expires { get; set; }
    }
}
