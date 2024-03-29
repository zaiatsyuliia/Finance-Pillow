using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class LoginViewModel
    {
        // Login fields
        [Required(ErrorMessage = "Please enter your login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
