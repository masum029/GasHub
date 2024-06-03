namespace GasHub.Models
{
    public class Role : BaseModel
    {
        public string RoleName { get; set; }
    }

    public enum UserRole
    {
        Admin,
        User,
        Moderator,
        Professional
    }
}
