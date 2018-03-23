using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Core.Domain
{
    public class ProvidedUtility
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int UtilityId { get; set; }
        public Utility Utility { get; set; }
        public ICollection<Tariff> Tariffs { get; set; }

        public ProvidedUtility()
        {
            Tariffs = new Collection<Tariff>();
        }
    }
}
