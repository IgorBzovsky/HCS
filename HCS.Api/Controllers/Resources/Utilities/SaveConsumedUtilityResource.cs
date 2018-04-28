namespace HCS.Api.Controllers.Resources.Utilities
{
    public class SaveConsumedUtilityResource
    {
        public int ProvidedUtilityId { get; set; }
        public decimal? Subsidy { get; set; }
        public double? Consumption { get; set; }
        public bool HasMeter { get; set; }
    }
}
