namespace HCS.Core.Domain
{
    public class ConsumedUtility
    {
        public int Id { get; set; }
        public decimal? ObligatoryPrice { get; set; }
        public int ProvidedUtilityId { get; set; }
        public ProvidedUtility ProvidedUtility { get; set; }
        public int? TariffId { get; set; }
        public Tariff Tariff { get; set; }
        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }
        public bool HasMeter { get; set; }
    }
}
