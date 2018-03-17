using System;
using System.Collections.Generic;
using System.Text;

namespace HCS.Core.Domain
{
    public class ConsumerCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConsumerTypeId { get; set; }
        public ConsumerType ConsumerType { get; set; }
    }
}
