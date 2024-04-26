namespace Business.DTO;

public class SettingsDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public int ExpenseLimit { get; set; }
    public int IncomeLimit { get; set; }
}