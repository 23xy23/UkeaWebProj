using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9]{3,15}$", ErrorMessage = "Invalid username. It must be 3-15 characters long and alphanumeric.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Invalid password. It must be at least 8 characters long and contain at least one letter and one number.")]
        public string Password { get; set; }
    }
}
