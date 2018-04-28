namespace HCS.Api.Controllers.Resources.UtilityBills
{
    public class UtilityBillListItemResource
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Total { get; set; }
    }
}
