using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers.Resources.Location
{
    public class LocationResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Building { get; set; }
        public string Appartment { get; set; }
        public LocationResource Parent { get; set; }
    }
}
