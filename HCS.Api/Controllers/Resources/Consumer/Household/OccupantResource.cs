using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Api.Controllers.Resources.Consumer.Household
{
    public class OccupantResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public ExemptionResource Exemption { get; set; }
        public ICollection<ConsumptionNormResource> ConsumptionNorms { get; set; }

        public OccupantResource()
        {
            ConsumptionNorms = new Collection<ConsumptionNormResource>();
        }
    }
}
