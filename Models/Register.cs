﻿namespace GasHub.Models
{
    public class Register  : BaseModel
    {
        public string FirstName { get; set; }
        public string LaststName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
        public List<string> Roles { get; set; }
    }
    public enum ByAdminRole
    {
        Admin,
        User,
        Moderator,
        Professional,
        Professional1,
        Professional2
    }
}
