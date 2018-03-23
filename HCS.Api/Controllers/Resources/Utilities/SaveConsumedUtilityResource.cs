namespace HCS.Api.Controllers.Resources.Utilities
{
    public class SaveConsumedUtilityResource
    {
        public int ProvidedUtilityId { get; set; }
        public decimal? ObligatoryPrice { get; set; }
        public bool HasMeter { get; set; }
    }
}
