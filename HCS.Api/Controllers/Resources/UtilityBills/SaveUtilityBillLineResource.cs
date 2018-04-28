namespace HCS.Api.Controllers.Resources.UtilityBills
{
    public class SaveUtilityBillLineResource
    {
        public int Id { get; set; }
        public double MeterReadingEnd { get; set; }
        public int ConsumedUtilityId { get; set; }
    }
}
