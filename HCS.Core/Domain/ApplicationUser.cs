using Microsoft.AspNetCore.Identity;

namespace HCS.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool IsDeleted { get; set; }
        public int? ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
