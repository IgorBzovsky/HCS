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

            CreateMap<Provider, SaveProviderResource>()
                .ForMember(pr => pr.ProvidedUtilities,
                opt => opt.MapFrom(p => p.ProvidedUtilities
                .Select(x => x.UtilityId)));
            CreateMap<Provider, ProviderResource>()
                .ForMember(pr => pr.ProvidedUtilities,
                opt => opt.MapFrom(p => p.ProvidedUtilities
                .Select(x => new KeyValuePairResource { Id = x.Utility.Id, Name = x.Utility.Name })));

            //API resource to domain
            CreateMap<UserResource, ApplicationUser>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            CreateMap<SaveProviderResource, Provider>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.ProvidedUtilities, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    //Remove unselected provided utilities
                    var removedUtilities = p.ProvidedUtilities
                        .Where(x => !pr.ProvidedUtilities.Contains(x.UtilityId));
                    foreach (var removedUtility in removedUtilities)
                        p.ProvidedUtilities.Remove(removedUtility);

                    //Add new provided utility
                    var addedUtilities = pr.ProvidedUtilities
                        .Where(id => !p.ProvidedUtilities.Any(x => x.UtilityId == id))
                        .Select(id => new ProvidedUtility { UtilityId = id });
                    foreach (var addedUtility in addedUtilities)
                        p.ProvidedUtilities.Add(addedUtility);
                });
        }
    }
}
