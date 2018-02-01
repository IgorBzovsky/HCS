using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HCS.Core.Domain
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public ICollection<ProvidedUtility> ProvidedUtilities { get; set; }

        public Provider()
        {
            ProvidedUtilities = new Collection<ProvidedUtility>();
        }
    }
}
