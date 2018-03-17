using HCS.Api.Controllers.Resources.Location;

namespace HCS.Api.Controllers.Resources.Consumer
{
    public class ConsumerLocationResource
    {
        public int Id { get; set; }
        public KeyValuePairResource ConsumerType { get; set; }
        public string ApplicationUserId { get; set; }
        public LocationResource Location { get; set; }
    }
}
