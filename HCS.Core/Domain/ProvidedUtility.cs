using System;
using System.Collections.Generic;
using System.Text;

namespace HCS.Core.Domain
{
    public class ProvidedUtility
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int UtilityId { get; set; }
        public Utility Utility { get; set; }
    }
}
