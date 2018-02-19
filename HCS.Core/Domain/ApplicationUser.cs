﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCS.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public int? ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
