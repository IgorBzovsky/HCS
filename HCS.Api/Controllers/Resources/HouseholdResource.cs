﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers.Resources
{
    public class HouseholdResource
    {
        public int Id { get; set; }
        public double Area { get; set; }
        public bool HasElectricHeating { get; set; }
        public bool HasTowelRail { get; set; }
        public bool HasElectricHotplates { get; set; }
        public bool HasCentralGasSupply { get; set; }
        public int LocationId { get; set; }
    }
}
