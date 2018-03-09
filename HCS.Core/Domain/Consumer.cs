namespace HCS.Core.Domain
{
    public abstract class Consumer
    {
        public int Id { get; set; }
        public double Area { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
