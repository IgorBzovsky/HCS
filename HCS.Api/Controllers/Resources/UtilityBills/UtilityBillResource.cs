using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers.Resources.UtilityBills
{
    public class UtilityBillResource
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsMetersReading { get; set; }
        public int ConsumerId { get; set; }
        public ICollection<UtilityBillLineResource> UtilityBillLines { get; set; }
        public UtilityBillResource()
        {
            UtilityBillLines = new Collection<UtilityBillLineResource>();
        }
    }
}
