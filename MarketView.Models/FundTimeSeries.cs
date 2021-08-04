using System;
//using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Text;
using System.Text.Json.Serialization;

namespace MarketView.Models
{
    public class FundTimeSeries
    {
		public String FundName { get; set; }

		[JsonIgnore]
		public Dictionary<string, StatDictionary> TimeSeries { get; set; }

		public List<StatDictionary> TimeSeriesList 
		{ 
			get 
			{
				if (this.TimeSeries != null && this.TimeSeries.Count > 0)
				{
					return this.TimeSeries.Values.ToList();
				}
				return new List<StatDictionary>();
			} 
		}

		public FundTimeSeries()
		{
			TimeSeries = new Dictionary<string, StatDictionary>();
		}
	}
}
