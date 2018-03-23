using HCS.Api.Controllers.Resources.Tariff;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Api.Controllers.Resources.Utilities
{
    public class ProvidedUtilityResource
    {
        public int Id { get; set; }
        public int UtilityId { get; set; }
        public string Name { get; set; }
        public string MeasureUnit { get; set; }
        public bool IsSeasonal { get; set; }
        public ICollection<TariffInfoResource> Tariffs { get; set; }

        public ProvidedUtilityResource()
        {
            Tariffs = new Collection<TariffInfoResource>();
        }
    }
}
