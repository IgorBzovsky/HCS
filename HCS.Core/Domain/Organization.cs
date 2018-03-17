namespace HCS.Core.Domain
{
    public class Organization : Consumer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationCategoryId { get; set; }
        public OrganizationCategory OrganizationType { get; set; }
    }
}
