using System;
//using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Text;
using System.Text.Json.Serialization;


namespace MarketView.Models
{
    public class FundYearlySeries
    {
		public String FundName { get; set; }

		[JsonIgnore]
		public Dictionary<string, YearlyStat> TimeSeries { get; set; }

        public List<YearlyStat> TimeSeriesList
        {
            get
            {
                if (this.TimeSeries != null && this.TimeSeries.Count > 0)
                {
                    return this.TimeSeries.Values.ToList();
                }
                return new List<YearlyStat>();
            }
        }

        public FundYearlySeries()
		{
			TimeSeries = new Dictionary<string, YearlyStat>();
		}
	}
}
