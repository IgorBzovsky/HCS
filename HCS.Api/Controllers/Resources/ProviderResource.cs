﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers.Resources
{
    public class ProviderResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public KeyValuePairResource Location { get; set; }
        public ICollection<KeyValuePairResource> ProvidedUtilities { get; set; }

        public ProviderResource()
        {
            ProvidedUtilities = new Collection<KeyValuePairResource>();
        }
    }
}
