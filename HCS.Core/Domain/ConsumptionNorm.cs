namespace HCS.Core.Domain
{
    public class ConsumptionNorm
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int ConsumedUtilityId { get; set; }
        public ConsumedUtility ConsumedUtility { get; set; }
        public int OccupantId { get; set; }
    }
}
