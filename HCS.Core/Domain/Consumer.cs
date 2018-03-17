using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Core.Domain
{
    public class Consumer
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
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ConsumerCategoryId { get; set; }
        public ConsumerCategory ConsumerCategory { get; set; }
        public ICollection<ConsumedUtility> ConsumedUtilities { get; set; }
        
        public ICollection<Occupant> Occupants { get; set; }

        public Consumer()
        {
            ConsumedUtilities = new Collection<ConsumedUtility>();
            Occupants = new Collection<Occupant>();
        }

        public bool IsOfType(string typeName)
        {
            return ConsumerCategory.ConsumerType.Name.Equals(typeName, System.StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
