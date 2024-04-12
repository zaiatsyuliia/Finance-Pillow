using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO;

public class UserDto
{
    public int IdUser { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public int ExpenseLimit { get; set; }
    public int IncomeLimit { get; set; }
}
