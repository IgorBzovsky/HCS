namespace HCS.Core.Domain
{
    public class Block
    {
        public int Id { get; set; }
        public int Limit { get; set; }
        public decimal Price { get; set; }
        public int TariffId { get; set; }
        public Tariff Tariff { get; set; }
    }
}
