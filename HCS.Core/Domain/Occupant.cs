using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Core.Domain
{
    public class Occupant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int ConsumerId { get; set; }
        public Consumer Household { get; set; }
        public int ExemptionId { get; set; }
        public Exemption Exemption { get; set; }
        public ICollection<ConsumptionNorm> ConsumptionNorms { get; set; }

        public Occupant()
        {
            ConsumptionNorms = new Collection<ConsumptionNorm>();
        }
    }
}
