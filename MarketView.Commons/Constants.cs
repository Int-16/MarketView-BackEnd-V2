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

        public static string[] statsSupported = { "OneDayReturns", "OneWeekReturns", "OneMonthReturns", "OneQuarterReturns", "OneYearReturns", "WeekToDate", "MonthToDate", "YearToDate", "QuarterToDate", "LastWeekReturns", "LastMonthReturns", "LastYearReturns", "LastQuarterReturns" , "HistoricalCumulativeReturns", "AnnualReturns","AnnualizedReturn", "AnnualizedReturnLast1Year", "AnnualizedReturnLast3Year", "AnnualizedReturnLast5Year", "LastThreeMonthsReturns" };

    }
}
