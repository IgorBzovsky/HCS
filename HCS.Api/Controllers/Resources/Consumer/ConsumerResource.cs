using HCS.Api.Controllers.Resources.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Api.Controllers.Resources.Consumer
{
    public class ConsumerResource
    {
        public int Id { get; set; }
        public bool IsDraft { get; set; }
        public string OrganizationName { get; set; }
        public double Area { get; set; }
        public bool HasElectricHeating { get; set; }
        public bool HasTowelRail { get; set; }
        public bool HasElectricHotplates { get; set; }
        public bool HasCentralGasSupply { get; set; }
        public bool HasSubsidy { get; set; }
        public KeyValuePairResource ConsumerType { get; set; }
        public KeyValuePairResource ConsumerCategory { get; set; }
        public string ApplicationUserId { get; set; }
        public int LocationId { get; set; }
        public ICollection<ConsumedUtilityResource> ConsumedUtilities { get; set; }

        public ConsumerResource()
        {
            ConsumedUtilities = new Collection<ConsumedUtilityResource>();
        }
    }
}
