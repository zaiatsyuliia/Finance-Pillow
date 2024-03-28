using Business.DTO;

namespace Presentation.Models;
public class StatisticsViewModel
{
	public List<StatisticsDailyDto> MonthDaily { get; set; }

	public List<StatisticsTotalDto> MonthTotal { get; set; }

	public List<StatisticsMonthlyDto> SixMonthsMonthly { get; set; }

	public List<StatisticsTotalDto> SixMonthsTotal { get; set; }

	public List<StatisticsMonthlyDto> YearMonthly { get; set; }

	public List<StatisticsTotalDto> YearTotal { get; set; }
}
