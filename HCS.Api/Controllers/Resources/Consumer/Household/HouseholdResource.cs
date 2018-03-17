using HCS.Api.Controllers.Resources.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Api.Controllers.Resources.Consumer.Household
{
    public class HouseholdResource
    {
        public int Id { get; set; }
        public double Area { get; set; }
        public bool HasElectricHeating { get; set; }
        public bool HasTowelRail { get; set; }
        public bool HasElectricHotplates { get; set; }
        public bool HasCentralGasSupply { get; set; }
        public bool HasSubsidy { get; set; }
        public string ApplicationUserId { get; set; }
        public int LocationId { get; set; }
        public string Discriminator { get; set; }
        public ICollection<ConsumedUtilityResource> ConsumedUtilities { get; set; }

        public HouseholdResource()
        {
            ConsumedUtilities = new Collection<ConsumedUtilityResource>();
        }
    }
}
