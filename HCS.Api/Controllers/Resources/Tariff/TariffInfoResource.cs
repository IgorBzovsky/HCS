using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers.Resources.Tariff
{
    public class TariffInfoResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ConsumerType { get; set; }
    }
}
