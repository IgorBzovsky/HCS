using HCS.Api.Controllers.Resources.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Api.Controllers.Resources.Consumer
{
    public class ConsumerInfoResource
    {
        public int Id { get; set; }
        public bool HasSubsidy { get; set; }
        public int LocationId { get; set; }
        public ICollection<ConsumedUtilityInfoResource> ConsumedUtilities { get; set; }

        public ConsumerInfoResource()
        {
            ConsumedUtilities = new Collection<ConsumedUtilityInfoResource>();
        }
    }
}
