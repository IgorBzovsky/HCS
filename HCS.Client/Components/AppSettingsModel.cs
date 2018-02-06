using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Client.Components
{
    public class AppSettingsModel
    {
        public string ApiUrl { get; set; }
        public string AuthUrl { get; set; }
        public string WebUrl { get; set; }
        public string RedirectUrl { get; set; }
        public string AccessToken { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public bool AdminEnabled { get; set; }
    }
}
