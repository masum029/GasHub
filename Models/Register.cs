using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GasHub.Models
{
    public class Register  : BaseModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        [DisplayName("Last Name")]
        public string LaststName { get; set; }
        [Required(ErrorMessage = "User Name is Required")]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phome Number is Required")]
        [DisplayName("Phone")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DisplayName("Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DisplayName("Confirm Password")]
        public string ConfirmationPassword { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        [DisplayName("Role Name")]
        public List<string> Roles { get; set; }
    }
    
}
