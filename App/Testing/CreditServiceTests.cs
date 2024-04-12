using Xunit;
using Business.Services;
using Business.DTO;

namespace Testing;

public class CreditServiceTests
{
    private readonly CreditService _creditService;

    public CreditServiceTests()
    {
        _creditService = new CreditService();
    }

    [Theory]
    [InlineData(12, 10000, 5, 856.07, 10272.90, 272.90)]
    [InlineData(24, 5000, 10, 230.72, 5537.39, 537.39)]
    [InlineData(0, 2000, 5, 0, 0, 0)] // Testing zero term
    [InlineData(12, 0, 5, 0, 0, 0)]      // Testing zero amount
    [InlineData(12, 10000, 0, 833.33, 10000, 0)] // Testing zero rate
    public void CalculateCredit_ReturnsExpectedResult(
        int term, decimal amount, decimal rate,
        decimal expectedMonthlyPayment, decimal expectedTotalPayment, decimal expectedTotalInterest)
    {
        // Act
        var result = _creditService.CalculateCredit(term, amount, rate);

        // Assert
        Assert.Equal(expectedMonthlyPayment, result.MonthlyPayment);
        Assert.Equal(expectedTotalPayment, result.TotalPayment);
        Assert.Equal(expectedTotalInterest, result.TotalInterest);
    }

    [Fact]
    public void CalculateCredit_WhenRateCausesDivisionByZero_ReturnsCorrectDefault()
    {
        // Arrange
        int term = 360; // 30 years
        decimal amount = 200000;
        decimal rate = 0;

        // Act
        var result = _creditService.CalculateCredit(term, amount, rate);

        // Assert - Special case where monthlyRate exactly equals 0 (handled separately if dividing by zero)
        Assert.Equal(555.56M, Math.Round(result.MonthlyPayment, 2));
        Assert.Equal(200000M, result.TotalPayment);
        Assert.Equal(0M, result.TotalInterest);
    }
}
