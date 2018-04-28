using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Api.Controllers.Resources.Consumer;
using HCS.Api.Controllers.Resources.Consumer.Household;
using HCS.Api.Controllers.Resources.Location;
using HCS.Api.Controllers.Resources.Provider;
using HCS.Api.Controllers.Resources.Queries;
using HCS.Api.Controllers.Resources.Tariff;
using HCS.Api.Controllers.Resources.User;
using HCS.Api.Controllers.Resources.Utilities;
using HCS.Api.Controllers.Resources.UtilityBills;
using HCS.Core.Domain;
using HCS.Core.Queries;
using System.Collections.Generic;
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
            CreateMap<Utility, UtilityResource>();
            CreateMap<ProvidedUtility, ProvidedUtilityResource>()
                .ForMember(pr => pr.Name, opt => opt.MapFrom(p => p.Utility.Name))
                .ForMember(pr => pr.MeasureUnit, opt => opt.MapFrom(p => p.Utility.MeasureUnit))
                .ForMember(pr => pr.IsSeasonal, opt => opt.MapFrom(p => p.Utility.IsSeasonal))
                .ForMember(pr => pr.Tariffs, opt => opt.MapFrom(p =>
                Mapper.Map<IEnumerable<Tariff>, IEnumerable<TariffInfoResource>>(p.Tariffs)));
            CreateMap<ConsumedUtility, ConsumedUtilityInfoResource>()
                .ForMember(cr => cr.IsSeasonal, opt => opt.MapFrom(c => c.ProvidedUtility.Utility.IsSeasonal))
                .ForMember(cr => cr.Name, opt => opt.MapFrom(c => c.ProvidedUtility.Utility.Name))
                .ForMember(cr => cr.MeasureUnit, opt => opt.MapFrom(c => c.ProvidedUtility.Utility.MeasureUnit));


            //ApplicationUser
            CreateMap<ApplicationUser, UserResource>();

            //Provider
            CreateMap<Provider, ProviderResource>()
               .ForMember(pr => pr.ProvidedUtilities,
               opt => opt.MapFrom(p =>
                Mapper.Map<IEnumerable<ProvidedUtility>, IEnumerable<ProvidedUtilityResource>>(p.ProvidedUtilities)));

            //ConsumerCategory
            CreateMap<ConsumerCategory, KeyValuePairResource>();

            //ConsumerType
            CreateMap<ConsumerType, KeyValuePairResource>();

            //Consumer
            CreateMap<Consumer, ConsumerLocationResource>()
                .ForMember(cr => cr.ConsumerType, opt => opt.MapFrom(c => new KeyValuePairResource
                {
                    Id = c.ConsumerCategory.ConsumerType.Id,
                    Name = c.ConsumerCategory.ConsumerType.Name
                }));
            CreateMap<Consumer, ConsumerInfoResource>();
            CreateMap<Consumer, ConsumerResource>()
                .ForMember(cr => cr.ConsumerType, opt => opt.MapFrom(c => new KeyValuePairResource {
                    Id = c.ConsumerCategory.ConsumerType.Id,
                    Name = c.ConsumerCategory.ConsumerType.Name
                }))
                .ForMember(cr => cr.ConsumedUtilities,
               opt => opt.MapFrom(c => c.ConsumedUtilities
               .Select(x => new ConsumedUtilityResource { Id = x.Id, ProvidedUtilityId = x.ProvidedUtility.Id, Name = x.ProvidedUtility.Utility.Name, MeasureUnit = x.ProvidedUtility.Utility.MeasureUnit, Subsidy = x.Subsidy, TariffId = x.TariffId, HasMeter = x.HasMeter, IsSeasonal = x.ProvidedUtility.Utility.IsSeasonal, Consumption = x.Consumption })));

            //Occupant
            CreateMap<Occupant, OccupantResource>()
                .ForMember(or => or.ConsumptionNorms,
               opt => opt.MapFrom(n => n.ConsumptionNorms
               .Select(x => new ConsumptionNormResource { Id = x.Id, Amount = x.Amount, ConsumedUtilityId = x.ConsumedUtilityId, IsSeasonal = x.IsSeasonal, UtilityName = x.ConsumedUtility.ProvidedUtility.Utility.Name, MeasureUnit = x.ConsumedUtility.ProvidedUtility.Utility.MeasureUnit })));

            //Exemption 
            CreateMap<Exemption, ExemptionResource>();

            //Tariff
            CreateMap<Block, BlockResource>();
            CreateMap<Tariff, TariffResource>();
            CreateMap<Tariff, TariffInfoResource>()
                .ForMember(tr => tr.ConsumerType, opt => opt.MapFrom(t => t.ConsumerType.Name));
            CreateMap<Tariff, TariffListItemResource>()
                .ForMember(tr => tr.ProvidedUtility, opt => opt.MapFrom(t => t.ProvidedUtility.Utility.Name))
                .ForMember(tr => tr.ConsumerType, opt => opt.MapFrom(t => t.ConsumerType.Name))
                .ForMember(tr => tr.ConsumersQuantity, opt => opt.MapFrom(t => t.ConsumedUtilities.Count));

            //UtilityBillLine
            CreateMap<UtilityBillLine, UtilityBillLineResource>();


            //UtilityBill
            CreateMap<UtilityBill, UtilityBillResource>();
            CreateMap<UtilityBill, UtilityBillListItemResource>()
                .AfterMap((u, ur) => ur.Total = u.GetTotal());

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
                        .Select(x => new ConsumedUtility { ProvidedUtilityId = x.ProvidedUtilityId, Subsidy = x.Subsidy, HasMeter = x.HasMeter, TariffId = x.TariffId, Consumption = x.Consumption }).ToList();
                    foreach (var addedUtility in addedUtilities)
                        h.ConsumedUtilities.Add(addedUtility);

                    //Map existing consumed utilities
                    foreach (var utility in h.ConsumedUtilities)
                    {
                        var utilityResource = hr.ConsumedUtilities.FirstOrDefault(x => x.ProvidedUtilityId == utility.ProvidedUtilityId);
                        Mapper.Map(utilityResource, utility);
                    }
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

            //Tariff
            CreateMap<BlockResource, Block>()
                .ForMember(t => t.Id, opt => opt.Ignore());
            CreateMap<TariffResource, Tariff>()
                .ForMember(t => t.Id, opt => opt.Ignore());

            //UtilityBillLine
            CreateMap<SaveUtilityBillLineResource, UtilityBillLine>()
                .ForMember(u => u.Id, opt => opt.Ignore());


            //UtilityBill
            CreateMap<SaveUtilityBillResource, UtilityBill>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            //Queries
            CreateMap<UserQueryResource, UserQuery>();
        }
    }
}
