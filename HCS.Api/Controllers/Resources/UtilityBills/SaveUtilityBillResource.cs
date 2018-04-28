using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Api.Controllers.Resources.UtilityBills
{
    public class SaveUtilityBillResource
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public bool IsMetersReading { get; set; }
        public int ConsumerId { get; set; }
        public ICollection<SaveUtilityBillLineResource> UtilityBillLines { get; set; }
        public SaveUtilityBillResource()
        {
            UtilityBillLines = new Collection<SaveUtilityBillLineResource>();
        }
    }
}
