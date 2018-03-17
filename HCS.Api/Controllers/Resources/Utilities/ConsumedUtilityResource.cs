namespace HCS.Api.Controllers.Resources.Utilities
{
    public class ConsumedUtilityResource
    {
        public int Id { get; set; }
        public int ProvidedUtilityId { get; set; }
        public bool IsSeasonal { get; set; }
        public string Name { get; set; }
        public decimal? ObligatoryPrice { get; set; }
    }
}
