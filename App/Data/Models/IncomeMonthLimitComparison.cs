﻿using System;

namespace Data.Models;

public class IncomeMonthLimitComparison
{
    public int IdUser { get; set; } // User ID

    public double TotalSum { get; set; } // Total sum of expenses for the user in the current month

    public int UserLimit { get; set; } // Limit set for the user

    public bool LimitStatus { get; set; } // Indicates whether the user's expenses exceed the limit

    // You can add other properties as needed based on your view's columns
}
