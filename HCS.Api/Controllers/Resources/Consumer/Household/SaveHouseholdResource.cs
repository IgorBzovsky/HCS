using HCS.Api.Controllers.Resources.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Api.Controllers.Resources.Consumer.Household
{
    public class SaveHouseholdResource
    {
        public int Id { get; set; }
        public double Area { get; set; }
        public bool HasElectricHeating { get; set; }
        public bool HasTowelRail { get; set; }
        public bool HasElectricHotplates { get; set; }
        public bool HasCentralGasSupply { get; set; }
        public string ApplicationUserId { get; set; }
        public int LocationId { get; set; }
        public ICollection<SaveConsumedUtilityResource> ConsumedUtilities { get; set; }

        public SaveHouseholdResource()
        {
            ConsumedUtilities = new Collection<SaveConsumedUtilityResource>();
        }
    }
}
