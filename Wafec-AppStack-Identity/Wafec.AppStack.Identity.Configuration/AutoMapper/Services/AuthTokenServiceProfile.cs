using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Service.Models.AuthTokenService;

namespace Wafec.AppStack.Identity.Configuration.AutoMapper.Services
{
    public class AuthTokenServiceProfile : Profile
    {
        public AuthTokenServiceProfile()
        {
            CreateMap<AuthToken, AuthTokenCreateView>();
        }
    }
}
