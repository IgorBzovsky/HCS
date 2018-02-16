using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api
{
    public class RolePolicies
    {
        public string RoleType { get; set; }
        public Policy AdminPolicy { get; set; }
        public Policy ProviderPolicy { get; set; }
        public Policy ConsumerPolicy { get; set; }
    }

    public class Policy
    {
        public string PolicyName { get; set; }
        public string RoleName { get; set; }
    }
}
