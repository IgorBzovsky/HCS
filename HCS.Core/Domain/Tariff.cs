using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Core.Domain
{
    public class Tariff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SubscriberFee { get; set; }
        public decimal Price { get; set; }
        public int ConsumerTypeId { get; set; }
        public ConsumerType ConsumerType { get; set; }
        public int ProvidedUtilityId { get; set; }
        public ProvidedUtility ProvidedUtility { get; set; }
        public ICollection<ConsumedUtility> ConsumedUtilities { get; set; }
        public ICollection<Block> Blocks { get; set; }

        public Tariff()
        {
            Blocks = new Collection<Block>();
            ConsumedUtilities = new Collection<ConsumedUtility>();
        }
    }
}
