using System;
using System.Collections.Generic;
using System.Text;

namespace HCS.Core.Domain
{
    public class Utility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MeasureUnitId { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
    }
}
