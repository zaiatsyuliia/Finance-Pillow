namespace Presentation.Models;

public class SettingsViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }

    public int ExpenseLimit { get; set; }
    public int IncomeLimit { get; set; }

    public string ErrorMessage { get; set; }
    public string SuccessMessage { get; set; }
}
