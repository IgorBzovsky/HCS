using HCS.Api.Controllers.Resources.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers.Resources.Provider
{
    public class ProviderResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public KeyValuePairResource Location { get; set; }
        public ICollection<ProvidedUtilityResource> ProvidedUtilities { get; set; }

        public ProviderResource()
        {
            ProvidedUtilities = new Collection<ProvidedUtilityResource>();
        }
    }
}
