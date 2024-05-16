using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public int ExpenseLimit { get; set; } = int.MaxValue;
    public int IncomeLimit { get; set; } = int.MaxValue;
}
