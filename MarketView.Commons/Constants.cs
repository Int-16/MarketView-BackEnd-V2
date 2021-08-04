using System;
using System.Collections.Generic;
using System.Text;

namespace MarketView.Commons
{
    public class Constants
    {
        public const int MongoDBDefaultPort = 27017;

        private static readonly Uri Endpoint = new Uri("http://localhost:54411/");

        public const string Nav = "nav";

        public static string[] statsSupported = { "1 Day Returns", "1 Week Returns", "1 Month Returns",  "1 Quarter Returns", "1 Year Returns", "WeekToDate", "MonthToDate", "YearToDate", "QuarterToDate", "LastWeekReturns", "LastMonthReturns", "LastYearReturns", "LastQuarterReturns" , "HistoricalCumulativeReturns", "AnnualReturns","AnnualizedReturn", "AnnualizedReturnLast1Year", "AnnualizedReturnLast3Year", "AnnualizedReturnLast5Year", "LastThreeMonthsReturns" };

    }
}
