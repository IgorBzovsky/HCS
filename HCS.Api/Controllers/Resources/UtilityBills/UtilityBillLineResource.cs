using HCS.Api.Controllers.Resources.Utilities;

namespace HCS.Api.Controllers.Resources.UtilityBills
{
    public class UtilityBillLineResource
    {
        public int Id { get; set; }
        public double MeterReadingStart { get; set; }
        public double MeterReadingEnd { get; set; }
        public decimal Price { get; set; }
        public ConsumedUtilityInfoResource ConsumedUtility { get; set; }
    }
}
