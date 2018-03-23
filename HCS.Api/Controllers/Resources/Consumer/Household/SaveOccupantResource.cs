using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Api.Controllers.Resources.Consumer.Household
{
    public class SaveOccupantResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int ConsumerId { get; set; }
        public int ExemptionId { get; set; }
        public ICollection<ConsumptionNormResource> ConsumptionNorms { get; set; }

        public SaveOccupantResource()
        {
            ConsumptionNorms = new Collection<ConsumptionNormResource>();
        }
    }
}
