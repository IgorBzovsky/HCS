using HCS.Api.Controllers.Resources.Tariff;

namespace HCS.Api.Controllers.Resources.Utilities
{
    public class ConsumedUtilityInfoResource
    {
        public int Id { get; set; }
        public bool HasMeter { get; set; }
        public TariffResource Tariff { get; set; }
        public decimal? Subsidy { get; set; }
        public bool IsSeasonal { get; set; }
        public double? Consumption { get; set; }
        public string Name { get; set; }
        public string MeasureUnit { get; set; }
    }
}
