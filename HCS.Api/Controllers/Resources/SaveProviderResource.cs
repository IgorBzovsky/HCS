using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers.Resources
{
    public class SaveProviderResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public ICollection<int> ProvidedUtilities { get; set; }

        public SaveProviderResource()
        {
            ProvidedUtilities = new Collection<int>();
        }
    }
}
