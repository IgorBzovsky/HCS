namespace HCS.Api.Controllers.Resources
{
    public class SaveAddressResource
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public string Appartment { get; set; }
        public int ParentId { get; set; }
    }
}
