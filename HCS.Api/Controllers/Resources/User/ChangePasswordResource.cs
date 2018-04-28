namespace HCS.Api.Controllers.Resources.User
{
    public class ChangePasswordResource
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
