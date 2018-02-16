using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api
{
    public class AppSettings
    {
        public string ApiName { get; set; }
        public BaseUrl BaseUrl { get; set; }
        public string RedirectUrl { get; set; }
        public IdentityClient IdentityClient { get; set; }
        public string RoleClaimType { get; set; }
    }

    public class BaseUrl
    {
        public string Web { get; set; }
        public string Auth { get; set; }
        public string Api { get; set; }
    }

    public class IdentityClient
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
