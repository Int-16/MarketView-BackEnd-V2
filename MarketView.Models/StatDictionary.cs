using System;
using System.Collections.Generic;
using System.Text;

namespace MarketView.Models
{
    public class StatDictionary
    {
        public Dictionary<string, double> Stats { get; set; }

        public String Date { get; set; }

        public StatDictionary()
        {
            Stats = new Dictionary<string, double>();
        }
    }
}
