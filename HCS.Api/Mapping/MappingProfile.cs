using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Api.Controllers.Resources.Consumer.Household;
using HCS.Api.Controllers.Resources.Location;
using HCS.Api.Controllers.Resources.Provider;
using HCS.Api.Controllers.Resources.User;
using HCS.Core.Domain;
using System.Linq;

namespace HCS.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API resource
            
            //Location
            CreateMap<Location, KeyValuePairResource>();
            CreateMap<Location, LocationResource>();
            CreateMap<Location, SaveLocationResource>();

            //Utility
            CreateMap<Utility, KeyValuePairResource>();

            //ApplicationUser
            CreateMap<ApplicationUser, UserResource>();

            //Provider
            CreateMap<Provider, ProviderResource>()
               .ForMember(pr => pr.ProvidedUtilities,
               opt => opt.MapFrom(p => p.ProvidedUtilities
               .Select(x => new KeyValuePairResource { Id = x.Utility.Id, Name = x.Utility.Name })));
            CreateMap<Provider, SaveProviderResource>()
                .ForMember(pr => pr.ProvidedUtilities,
                opt => opt.MapFrom(p => p.ProvidedUtilities
                .Select(x => x.UtilityId)));
           
            //Household
            CreateMap<Household, SaveHouseholdResource>();
            CreateMap<Household, HouseholdResource>()
                .AfterMap((h, hr) => hr.Discriminator = "Household");
            

            //API resource to domain

            //Location
            CreateMap<SaveLocationResource, Location>()
                .ForMember(s => s.Id, opt => opt.Ignore());

            //ApplicationUser
            CreateMap<UserResource, ApplicationUser>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            //Provider
            CreateMap<SaveProviderResource, Provider>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.ProvidedUtilities, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    //Remove unselected provided utilities
                    var removedUtilities = p.ProvidedUtilities
                        .Where(x => !pr.ProvidedUtilities.Contains(x.UtilityId)).ToList();
                    foreach (var removedUtility in removedUtilities)
                        p.ProvidedUtilities.Remove(removedUtility);

                    //Add new provided utility
                    var addedUtilities = pr.ProvidedUtilities
                        .Where(id => !p.ProvidedUtilities.Any(x => x.UtilityId == id))
                        .Select(id => new ProvidedUtility { UtilityId = id }).ToList();
                    foreach (var addedUtility in addedUtilities)
                        p.ProvidedUtilities.Add(addedUtility);
                });

            //Household
            CreateMap<HouseholdResource, Household>()
                .ForMember(hr => hr.Id, opt => opt.Ignore());
            CreateMap<SaveHouseholdResource, Household>()
                .ForMember(h => h.Id, opt => opt.Ignore());
        }
    }
}
