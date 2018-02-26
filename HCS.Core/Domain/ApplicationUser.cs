using Microsoft.AspNetCore.Identity;

namespace HCS.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public int? ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
