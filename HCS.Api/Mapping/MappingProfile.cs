using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace HCS.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API resource
            CreateMap<Location, KeyValuePairResource>();
            CreateMap<Utility, KeyValuePairResource>();
            CreateMap<ApplicationUser, UserResource>()
                .ForMember(ur => ur.Roles, opt => opt.Ignore())
                .ForMember(ur => ur.Password, opt => opt.Ignore());

            //API resource to domain
            CreateMap<UserResource, ApplicationUser>()
                .ForMember(u => u.Id, opt => opt.Ignore());
        }
    }
}
