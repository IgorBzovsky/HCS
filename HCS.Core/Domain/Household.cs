using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Core.Domain
{
    public class Household : Consumer
    {
        public bool HasElectricHeating { get; set; }
        public bool HasTowelRail { get; set; }
        public bool HasElectricHotplates { get; set; }
        public bool HasCentralGasSupply { get; set; }
        public ICollection<Occupant> Occupants { get; set; }

        public Household()
        {
            Occupants = new Collection<Occupant>();
        }
    }
}
