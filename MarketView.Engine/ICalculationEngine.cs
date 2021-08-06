using MarketView.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketView.Engine
{
    public interface ICalculationEngine
    {
        List<MutualFund> GetFundById(string fundId);
        FundTimeSeries ReturnsCalculator(string fundId, List<string> statName);
        FundYearlySeries MonthlyTimeSeries(string fundId);

    }
}
