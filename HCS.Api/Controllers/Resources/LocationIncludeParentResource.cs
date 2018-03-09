using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers.Resources
{
    public class LocationIncludeParentResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LocationIncludeParentResource Parent { get; set; }
        public string Building { get; set; }
        public string Appartment { get; set; }
    }
}
