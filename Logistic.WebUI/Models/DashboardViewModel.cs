using System.Collections.Generic;

namespace Logistic.WebUI.Models
{
    public class DashboardViewModel
    {
        public int TodayRoutesCount { get; set; }

        public int YesterdayRoutesCount { get; set; }

        public int MonthRoutesCount { get; set; }

        public List<(string RateName, int AverageFilling)> Filling { get; set; }
    }
}