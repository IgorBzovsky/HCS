using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Api.Controllers.Resources.Consumer;
using HCS.Api.Controllers.Resources.Consumer.Household;
using HCS.Api.Controllers.Resources.Location;
using HCS.Api.Controllers.Resources.Provider;
using HCS.Api.Controllers.Resources.User;
using HCS.Api.Controllers.Resources.Utilities;
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
               .Select(x => new ProvidedUtilityResource { Id = x.Id, UtilityId = x.Utility.Id, Name = x.Utility.Name})));

            //ConsumerCategory
            CreateMap<ConsumerCategory, KeyValuePairResource>();

            //Consumer
            CreateMap<Consumer, ConsumerLocationResource>()
                .ForMember(cr => cr.ConsumerType, opt => opt.MapFrom(c => new KeyValuePairResource
                {
                    Id = c.ConsumerCategory.ConsumerType.Id,
                    Name = c.ConsumerCategory.ConsumerType.Name
                }));
            CreateMap<Consumer, ConsumerResource>()
                .ForMember(cr => cr.ConsumerType, opt => opt.MapFrom(c => new KeyValuePairResource {
                    Id = c.ConsumerCategory.ConsumerType.Id,
                    Name = c.ConsumerCategory.ConsumerType.Name
                }))
                .ForMember(cr => cr.ConsumedUtilities,
               opt => opt.MapFrom(c => c.ConsumedUtilities
               .Select(x => new ConsumedUtilityResource { Id = x.Id, ProvidedUtilityId = x.ProvidedUtility.Id, Name = x.ProvidedUtility.Utility.Name, ObligatoryPrice = x.ObligatoryPrice })));

            //Occupant
            CreateMap<Occupant, OccupantResource>()
                .ForMember(or => or.ConsumptionNorms,
               opt => opt.MapFrom(n => n.ConsumptionNorms
               .Select(x => new ConsumptionNormResource { Id = x.Id, Amount = x.Amount, ConsumedUtilityId = x.ConsumedUtilityId })));

            //Exemption 
            CreateMap<Exemption, ExemptionResource>();
            


            //API resource to domain

            //Location
            CreateMap<SaveLocationResource, Location>()
                .ForMember(s => s.Id, opt => opt.Ignore());

            //ApplicationUser
            CreateMap<UserResource, ApplicationUser>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            //Utility
            CreateMap<ConsumedUtilityResource, ConsumedUtility>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            //ConsumptionNorm
            CreateMap<ConsumptionNormResource, ConsumptionNorm>()
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

                    //Add new provided utilities
                    var addedUtilities = pr.ProvidedUtilities
                        .Where(id => !p.ProvidedUtilities.Any(x => x.UtilityId == id))
                        .Select(id => new ProvidedUtility { UtilityId = id }).ToList();
                    foreach (var addedUtility in addedUtilities)
                        p.ProvidedUtilities.Add(addedUtility);
                });

            //Consumer
            CreateMap<ConsumerResource, Consumer>()
                .ForMember(h => h.Id, opt => opt.Ignore())
                .ForMember(h => h.ConsumerCategory, opt => opt.Ignore())
                .ForMember(h => h.ConsumerCategoryId, opt => opt.MapFrom(c => c.ConsumerCategory.Id))
                .ForMember(h => h.ConsumedUtilities, opt => opt.Ignore())
                .AfterMap((hr, h) =>
                {
                    //Remove unselected consumed utilities
                    var removedUtilities = h.ConsumedUtilities
                        .Where(x => !hr.ConsumedUtilities.Any(u => u.ProvidedUtilityId == x.ProvidedUtilityId)).ToList();
                    foreach (var removedUtility in removedUtilities)
                        h.ConsumedUtilities.Remove(removedUtility);

                    //Add new consumed utilities
                    var addedUtilities = hr.ConsumedUtilities
                        .Where(u => !h.ConsumedUtilities.Any(x => x.ProvidedUtilityId == u.ProvidedUtilityId))
                        .Select(x => new ConsumedUtility { ProvidedUtilityId = x.ProvidedUtilityId, ObligatoryPrice = x.ObligatoryPrice }).ToList();
                    foreach (var addedUtility in addedUtilities)
                        h.ConsumedUtilities.Add(addedUtility);

                    //Map existing consumed utilities
                    Mapper.Map(hr.ConsumedUtilities, h.ConsumedUtilities);
                });

            //Occupant
            CreateMap<SaveOccupantResource, Occupant>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.ConsumptionNorms, opt => opt.Ignore())
                .AfterMap((or, o) =>
                {
                    //Remove unselected norms
                    var removedNorms = o.ConsumptionNorms
                        .Where(x => !or.ConsumptionNorms.Any(u => u.ConsumedUtilityId == x.ConsumedUtilityId)).ToList();
                    foreach (var removedNorm in removedNorms)
                        o.ConsumptionNorms.Remove(removedNorm);

                    //Add new norms
                    var addedUtilities = or.ConsumptionNorms
                        .Where(u => !o.ConsumptionNorms.Any(x => x.ConsumedUtilityId == u.ConsumedUtilityId))
                        .Select(x => new ConsumptionNorm { ConsumedUtilityId = x.ConsumedUtilityId, Amount = x.Amount }).ToList();
                    foreach (var addedUtility in addedUtilities)
                        o.ConsumptionNorms.Add(addedUtility);

                    //Map existing norms
                    Mapper.Map(or.ConsumptionNorms, o.ConsumptionNorms);
                });
        }
    }
}
