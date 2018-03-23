namespace HCS.Api.Controllers.Resources.Tariff
{
    public class TariffListItemResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProvidedUtility { get; set; }
        public string ConsumerType { get; set; }
        public int ConsumersQuantity { get; set; }
    }
}
