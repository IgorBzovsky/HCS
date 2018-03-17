namespace HCS.Api.Controllers.Resources.Consumer.Household
{
    public class ConsumptionNormResource
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int ConsumedUtilityId { get; set; }
    }
}