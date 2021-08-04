using MarketView.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketView.Data
{
    public interface IMutualFundLibrary
    {
        List<MutualFund> GetAllMutualFundData();

        List<MutualFund> GetFundById(string fundId);

        FundTimeSeries ReturnsCalculator(string fundId);

    }
}
