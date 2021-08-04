using MarketView.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketView.Engine
{
    public interface IDataEngine
    {
        List<MutualFund> GetAllMutualFundData();
    }
}
