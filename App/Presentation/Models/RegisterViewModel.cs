using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Please enter your login.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Please enter your password.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please confirm your password.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
}
