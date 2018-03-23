using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Api.Controllers.Resources.Tariff
{
    public class TariffResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SubscriberFee { get; set; }
        public decimal Price { get; set; }
        public int ProvidedUtilityId { get; set; }
        public int ConsumerTypeId { get; set; }
        public ICollection<BlockResource> Blocks { get; set; }

        public TariffResource()
        {
            Blocks = new Collection<BlockResource>();
        }
    }
}
