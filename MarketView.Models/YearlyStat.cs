using System;
using System.Collections.Generic;
using System.Text;

namespace MarketView.Models
{
    public class YearlyStat
    {
        public Dictionary<string, double> YearStats { get; set; }

        public YearlyStat()
        {
            YearStats = new Dictionary<string, double>();
        }
    }
}
