using MarketView.Commons;
using MarketView.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MarketView.Engine
{
    public class CalculationEngine : ICalculationEngine
    {
        public Data.IMutualFundLibrary MutualFundLibrary { get; set; }
        
        readonly string TypeName;

        public CalculationEngine(Data.IMutualFundLibrary mutualFundLibrary)
        {
            this.MutualFundLibrary = mutualFundLibrary;
            TypeName = this.GetType().Name;
        }

        private bool IsWeekend(string dates)
        {
            DateTime date = Convert.ToDateTime(dates);

            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }

        public DateTime SubtractDays(string dates, int days)
        {
            DateTime date = Convert.ToDateTime(dates);
            
            date = date.AddDays(days);

            while(IsWeekend(date.ToString()))
            {
                if (days > 0)
                {
                    date = date.AddDays(1);
                }
                else
                {
                    date = date.AddDays(-1);
                }
            }
            return date;
        }

        public string FormatToString(DateTime dates)
        { 
            return dates.ToString("yyyy-MM-dd");
        }

        public DateTime FormatToDate(string dates)
        {
            return Convert.ToDateTime(dates);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MutualFund> GetFundById(string fundId)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine GetFundById");
            List<MutualFund> resultList = this.MutualFundLibrary.GetFundById(fundId);
            return resultList;
        }

        public FundTimeSeries OneDayReturn(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine OneDayReturn");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                DateTime subtractedDate = SubtractDays(presentDate, -1);
                var previousDate = FormatToString(subtractedDate);

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav) || Double.IsNaN(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[0], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[0], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine OneDayReturn");
            return dataList;

        }

        public FundTimeSeries OneWeekReturn(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine OneWeekReturn");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                DateTime subtractedDate = SubtractDays(presentDate, -5);
                var previousDate = FormatToString(subtractedDate);

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[1], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[1], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine OneWeekReturn");
            return dataList;
        }

        public FundTimeSeries OneMonthReturn(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine OneMonthReturn");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                DateTime subtractedDate = SubtractDays(presentDate, -22);
                var previousDate = FormatToString(subtractedDate);

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[2], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[2], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine OneMonthReturn");
            return dataList;
        }

        public FundTimeSeries OneQuarterReturn(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine OneQuarterReturn");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }
                DateTime subtractedDate = SubtractDays(presentDate, -66);
                var previousDate = FormatToString(subtractedDate);

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[3], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[3], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine OneQuarterReturn");
            return dataList;
        }

        public FundTimeSeries OneYearReturn(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine OneYearReturn");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                DateTime subtractedDate = SubtractDays(presentDate, -252);
                var previousDate = FormatToString(subtractedDate);

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[4], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[4], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine OneYearReturn");
            return dataList;
        }

        public FundTimeSeries WTD(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine WeekToDate");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                //-- WeekToDay Calculation
                //Subtract presentDate of the Week - presentDate starting of Week
                //Using the difference Calculate PreviousDate which is(StartWeek Monday)

                int diff = DayOfWeek.Monday - FormatToDate(presentDate).DayOfWeek;
                DateTime subtractedDate = FormatToDate(presentDate).AddDays(diff);
                var previousDate = FormatToString(subtractedDate);

                //If the Date is WeekEnd iterated to Calculate the Working Day
                while (IsWeekend(previousDate))
                {
                    DateTime previousNDate = FormatToDate(previousDate);
                    previousDate = FormatToString(previousNDate.AddDays(1));
                }

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[5], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[5], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine WeekToDate");
            return dataList;
        }

        public FundTimeSeries MTD(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine MonthToDate");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }
                DateTime subtractedDate = new DateTime(FormatToDate(presentDate).Year, FormatToDate(presentDate).Month, 1);
                var previousDate = FormatToString(subtractedDate);

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[6], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[6], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine MonthToDate");
            return dataList;
        }
        
        public FundTimeSeries YTD(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine YearToDate");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                //-- YearToDay Calculation
                DateTime previousNDate = new DateTime(FormatToDate(presentDate).Year, 1, 1);
                var previousDate = FormatToString(previousNDate);

                //If the Date is WeekEnd iterate to Calculate the Next Working Day
                while (IsWeekend(previousDate))
                {
                    DateTime previousNSDate = FormatToDate(previousDate);
                    previousDate = FormatToString(previousNSDate.AddDays(1));
                }

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[7], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[7], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine YearToDate");
            return dataList;
        }

        public FundTimeSeries QTD(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine QuarterToDate");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                //DateTime presentDate = DateTime.Parse(dateKey.ToString());
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                //-- QuaterToDay Calculation
                DateTime previousNDate = new DateTime(FormatToDate(presentDate).Year, (((FormatToDate(presentDate).Month - 1) / 3 + 1) - 1) * 3 + 1, 1);
                var previousDate = FormatToString(previousNDate);

                //If the Date is WeekEnd iterate to Calculate the Next Working Day
                while (IsWeekend(previousDate))
                {
                    DateTime previousNSDate = FormatToDate(previousDate);
                    previousDate = FormatToString(previousNSDate.AddDays(1));
                }

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[8], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[8], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine QuarterToDate");
            return dataList;
        }

        public FundTimeSeries LWR(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine LastWeekReturn");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                //fDate-FirstDate(Monday of LastWeek) //lDate-LastDate(Friday of LastWeek)
                DateTime fDate = FormatToDate(presentDate).AddDays(0 - Convert.ToInt32(FormatToDate(presentDate).DayOfWeek) - 6);
                DateTime lDate = FormatToDate(presentDate).AddDays(5 - Convert.ToInt32(FormatToDate(presentDate).DayOfWeek) - 7);

                int count = 0;
                //fDate calculated not present in the DataSet iterate two times to find the NextPresentDate
                while (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) == false && count <= 2)
                {
                    fDate = fDate.AddDays(1);
                    count++;
                }

                count = 0;
                //lDate calculated not present in the DataSet iterate two times to find the PreviousPresentDate
                while (dataList.TimeSeries.ContainsKey(FormatToString(lDate)) == false && count <= 2)
                {
                    lDate = lDate.AddDays(-1);
                    count++;
                }

                if (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) && dataList.TimeSeries.ContainsKey(FormatToString(lDate)))
                {
                    var presentNav = (dataList.TimeSeries[FormatToString(lDate)].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[FormatToString(fDate)].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[9], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[9], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine LastWeekReturn");
            return dataList;
        }

        public FundTimeSeries LMR(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine LastMonthReturn");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                //fDate-FirstDate(First Date of the LastMonth) //lDate-LastDate(Last Date of the LastMonth)
                var month = new DateTime(FormatToDate(presentDate).Year, FormatToDate(presentDate).Month, 1);
                DateTime fDate = month.AddMonths(-1);
                DateTime lDate = month.AddDays(-1);

                int count = 0;
                while (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) == false && count <= 2)
                {
                    fDate = fDate.AddDays(1);
                    count++;
                }

                count = 0;
                while (dataList.TimeSeries.ContainsKey(FormatToString(lDate)) == false && count <= 2)
                {
                    lDate = lDate.AddDays(-1);
                    count++;
                }

                if (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) && dataList.TimeSeries.ContainsKey(FormatToString(lDate)))
                {
                    var presentNav = (dataList.TimeSeries[FormatToString(lDate)].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[FormatToString(fDate)].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[10], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[10], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine LastMonthReturn");
            return dataList;
        }

        public FundTimeSeries LYR(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine LastYearReturn");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                //fDate-FirstDate(FirstDate of the LastYear) //lDate-LastDate(LastDate of the LastYear)
                DateTime fDate = new DateTime(FormatToDate(presentDate).Year - 1, 1, 1);
                DateTime lDate = new DateTime(FormatToDate(presentDate).Year - 1, 12, 31);

                int count = 0;
                while (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) == false && count <= 2)
                {
                    fDate = fDate.AddDays(1);
                    count++;
                }

                count = 0;
                while (dataList.TimeSeries.ContainsKey(FormatToString(lDate)) == false && count <= 2)
                {
                    lDate = lDate.AddDays(-1);
                    count++;
                }

                if (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) && dataList.TimeSeries.ContainsKey(FormatToString(lDate)))
                {
                    var presentNav = (dataList.TimeSeries[FormatToString(lDate)].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[FormatToString(fDate)].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[11], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[11], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine LastYearReturn");
            return dataList;
        }

        public FundTimeSeries LQR(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine LastQuarterReturn");
            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                //DateTime presentDate = DateTime.Parse(dateKey.ToString());
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                //fDate-FirstDate(FirstDate of the LastYear) //lDate-LastDate(LastDate of the LastYear)
                DateTime fDate = new DateTime(FormatToDate(presentDate).Year, (((FormatToDate(presentDate).Month - 1) / 3 + 1) - 1) * 3 + 1, 1);
                DateTime lDate = fDate.AddMonths(3).AddDays(-1);

                int count = 0;
                while (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) == false && count <= 2)
                {
                    fDate = fDate.AddDays(1);
                    count++;
                }

                count = 0;
                while (dataList.TimeSeries.ContainsKey(FormatToString(lDate)) == false && count <= 2)
                {
                    lDate = lDate.AddDays(-1);
                    count++;
                }

                if (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) && dataList.TimeSeries.ContainsKey(FormatToString(lDate)))
                {
                    var presentNav = (dataList.TimeSeries[FormatToString(lDate)].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[FormatToString(fDate)].Stats[Constants.Nav]);
                    var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[12], returnNav);
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[12], 0.0000);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine LastQuarterReturn");
            return dataList;
        }

        public FundTimeSeries HistoricalCumulativeReturn(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine HistoricalCumulativeReturn");

            var cummulativeReturn=0.000; 
            var principalValue=1000.00;

            foreach (var dateKey in dataList.TimeSeries.Keys)
            {
                DateTime initDate = DateTime.Parse(dateKey.ToString());
                var presentDate = FormatToString(initDate);

                if (IsWeekend(presentDate))
                {
                    continue;
                }

                DateTime subtractedDate = SubtractDays(presentDate, -1);
                var previousDate = FormatToString(subtractedDate);

                if (dataList.TimeSeries.ContainsKey(previousDate))
                {
                    var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                    var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                    var returnNav = ((presentNav / previousNav) - 1);

                    if (Double.IsInfinity(returnNav))
                    {
                        returnNav = 0.00;
                    }

                    cummulativeReturn = principalValue * (1 + returnNav);
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[13], cummulativeReturn);
                    principalValue = cummulativeReturn;
                }
                else
                {
                    dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[13], cummulativeReturn);
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine HistoricalCumulativeReturn");
            return dataList;
        }

        public double AnnualReturnCalculator(FundTimeSeries dataList,string presentDate,int totalDays,int numDays)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine AnnualReturns");
            var annualReturn = 0.00;

            DateTime subtractedDate = SubtractDays(presentDate, -numDays);

            int count = 0;
            while (dataList.TimeSeries.ContainsKey(FormatToString(subtractedDate)) == false && count <= 2)
            {
                subtractedDate = subtractedDate.AddDays(1);
                count++;
            }

            var previousDate = FormatToString(subtractedDate);

            if (dataList.TimeSeries.ContainsKey(previousDate))
            {
                var presentNav = (dataList.TimeSeries[presentDate].Stats[Constants.Nav]);
                var previousNav = (dataList.TimeSeries[previousDate].Stats[Constants.Nav]);
                var returnNav = (((presentNav / previousNav) - 1)) * 100;

                if (Double.IsInfinity(returnNav))
                {
                    returnNav = 0;
                }

                annualReturn = (double)Math.Pow((1 + returnNav), (totalDays / numDays)) - 1;
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine AnnualReturns");
            return annualReturn;
        }

        public double ThreeMonthReturnCalculator(FundTimeSeries dataList,string presentDate,int numMonth)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine ThreeMonthReturns");
            var threeMonthsReturn = 0.0;

            //fDate-FirstDate(First Date of the LastMonth) //lDate-LastDate(Last Date of the LastMonth)
            var month = new DateTime(FormatToDate(presentDate).Year, FormatToDate(presentDate).Month, numMonth);
            DateTime fDate = month.AddMonths(-numMonth);
            DateTime lDate = month.AddDays(-numMonth);

            int count = 0;
            while (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) == false && count <= 2)
            {
                fDate = fDate.AddDays(1);
                count++;
            }

            count = 0;
            while (dataList.TimeSeries.ContainsKey(FormatToString(lDate)) == false && count <= 2)
            {
                lDate = lDate.AddDays(-1);
                count++;
            }

            if (dataList.TimeSeries.ContainsKey(FormatToString(fDate)) && dataList.TimeSeries.ContainsKey(FormatToString(lDate)))
            {
                var presentNav = (dataList.TimeSeries[FormatToString(lDate)].Stats[Constants.Nav]);
                var previousNav = (dataList.TimeSeries[FormatToString(fDate)].Stats[Constants.Nav]);
                threeMonthsReturn = ((presentNav - previousNav) / previousNav) * 100;

                if (Double.IsInfinity(threeMonthsReturn))
                {
                    threeMonthsReturn = 0.00;
                }
            }
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine ThreeMonthReturns");
            return threeMonthsReturn;
        }

        public FundTimeSeries AnnualReturn(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine AnnualReturns");
            var dateKey = dataList.TimeSeries.Keys;

            DateTime initDate = DateTime.Parse(dateKey.FirstOrDefault().ToString());
            DateTime lastDate = DateTime.Parse(dateKey.LastOrDefault().ToString());

            int year = initDate.Year - lastDate.Year;
            var presentDate = FormatToString(initDate);

            var annualReturns   = AnnualReturnCalculator(dataList, presentDate,year*365,year*252);
            var oneYearAnnual   = AnnualReturnCalculator(dataList,presentDate,365,252);
            var threeYearAnnual = AnnualReturnCalculator(dataList, presentDate,1095,756);
            var fiveYearAnnual  = AnnualReturnCalculator(dataList, presentDate,1825,1260);
            
            dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[15], annualReturns);
            dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[16], oneYearAnnual);
            dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[17], threeYearAnnual);
            dataList.TimeSeries[presentDate].Stats.Add(Constants.statsSupported[18], fiveYearAnnual);

            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine AnnualReturns");
            return dataList;
        }

        public FundTimeSeries LastThreeMonthsReturn(FundTimeSeries dataList)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine ThreeMonthReturns");
            
            var dateKey = dataList.TimeSeries.Keys;
            DateTime initDate = DateTime.Parse(dateKey.FirstOrDefault().ToString());
            var presentDate = FormatToString(initDate);

            var oneMonth   = ThreeMonthReturnCalculator(dataList,presentDate,1);
            var twoMonth   = ThreeMonthReturnCalculator(dataList, presentDate, 2);
            var threeMonth = ThreeMonthReturnCalculator(dataList, presentDate, 3);

            dataList.TimeSeries[presentDate].Stats.Add(initDate.AddMonths(-1).ToString("MMM"), oneMonth);
            dataList.TimeSeries[presentDate].Stats.Add(initDate.AddMonths(-2).ToString("MMM"), twoMonth);
            dataList.TimeSeries[presentDate].Stats.Add(initDate.AddMonths(-3).ToString("MMM"), threeMonth);
            
            Logging.LogInfo(TypeName, "Control Ends inside CalculationEngine ThreeMonthReturns");
            return dataList;
        }

        //Monthly Time Series
        public FundYearlySeries MonthlyTimeSeries(string fundId)
        {
            FundYearlySeries list = new FundYearlySeries();
            FundTimeSeries dataList = this.MutualFundLibrary.ReturnsCalculator(fundId);
            var dateKey = dataList.TimeSeries.Keys;

            DateTime firstDate = DateTime.Parse(dateKey.LastOrDefault().ToString());
            DateTime lastDate = DateTime.Parse(dateKey.FirstOrDefault().ToString()); 
            //Difference between the FirstDate and LastDate in Years
            var yearsDiff = lastDate.Year - firstDate.Year;

            for (int yearCount = 0; yearCount <= yearsDiff; yearCount++)
            {
                YearlyStat stat = new YearlyStat();
                DateTime yearFirstDate = firstDate.AddYears(yearCount);
                stat.YearStats.Add("Year", yearFirstDate.Year);
                if (yearCount > 0)
                {
                    yearFirstDate = new DateTime(yearFirstDate.Year, 1, 1);
                }
                DateTime yearLastDate = new DateTime(yearFirstDate.Year, 12, 31);
                
                //Difference between the YearFirst and YearLast in Months
                var monthsDiff = ((yearLastDate.Year - yearFirstDate.Year) * 12) + yearLastDate.Month - yearFirstDate.Month;
                
                for (int monthCount = 0; monthCount <= monthsDiff; monthCount++)
                {
                    DateTime monthFirstDate = firstDate;
                    if (yearCount == 0 && monthCount == 0)
                    {
                        monthFirstDate = firstDate;
                    }
                    if (monthCount >= 0)
                    {
                        monthFirstDate = new DateTime(yearFirstDate.Year, yearFirstDate.Month, 1);
                        monthFirstDate = monthFirstDate.AddMonths(monthCount);
                    }

                    DateTime monthLastDate = new DateTime(monthFirstDate.Year, monthFirstDate.Month, DateTime.DaysInMonth(monthFirstDate.Year, monthFirstDate.Month));
                    
                    //NAV CALCULATIONS
                    int count = 0;
                    while (dataList.TimeSeries.ContainsKey(FormatToString(monthFirstDate)) == false && count <= 2)
                    {
                        monthFirstDate = monthFirstDate.AddDays(1);
                        count++;
                    }

                    count = 0;
                    while (dataList.TimeSeries.ContainsKey(FormatToString(monthLastDate)) == false && count <= 2)
                    {
                        monthLastDate = monthLastDate.AddDays(-1);
                        count++;
                    }

                    if (dataList.TimeSeries.ContainsKey(FormatToString(monthFirstDate)) && dataList.TimeSeries.ContainsKey(FormatToString(monthLastDate)))
                    {
                        var presentNav = (dataList.TimeSeries[FormatToString(monthLastDate)].Stats[Constants.Nav]);
                        var previousNav = (dataList.TimeSeries[FormatToString(monthFirstDate)].Stats[Constants.Nav]);
                        var returnNav = ((presentNav - previousNav) / previousNav) * 100;

                        if (Double.IsInfinity(returnNav))
                        {
                            returnNav = 0.00;
                        }
                        stat.YearStats.Add(monthFirstDate.ToString("MMM"),returnNav);
                    }
                    else
                    {
                        stat.YearStats.Add(monthFirstDate.ToString("MMM"), 0.000);
                    }
                }
                list.TimeSeries.Add(yearFirstDate.ToString(), stat);
            }
            list.FundName = dataList.FundName;
            return list;
        }

        /// <summary>
        /// Depending upon the statName specific SwitchCase gets executed in a Loop
        /// </summary>
        /// <returns>FundTimeSeries with a FundName and Stats</returns>
        public FundTimeSeries ReturnsCalculator(string fundId, List<string> statName)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationEngine ReturnsCalculator");

            FundTimeSeries dataList = this.MutualFundLibrary.ReturnsCalculator(fundId);

            foreach (var item in statName)
            {
                if (Constants.statsSupported.Contains(item,StringComparer.CurrentCultureIgnoreCase))
                {
                    switch (item)
                    {
                        case "OneDayReturns":
                            dataList = OneDayReturn(dataList);
                            break;

                        case "OneWeekReturns":
                            dataList = OneWeekReturn(dataList);
                            break;

                        case "OneMonthReturns":
                            dataList = OneMonthReturn(dataList);
                            break;

                        case "OneQuarterReturns":
                            dataList = OneQuarterReturn(dataList);
                            break;

                        case "OneYearReturns":
                            dataList = OneYearReturn(dataList);
                            break;

                        case "WeekToDate":
                            dataList = WTD(dataList);
                            break;

                        case "MonthToDate":
                            dataList = MTD(dataList);
                            break;

                        case "YearToDate":
                            dataList = YTD(dataList);
                            break;

                        case "QuarterToDate":
                            dataList = QTD(dataList);
                            break;

                        case "LastWeekReturns":
                            dataList = LWR(dataList);
                            break;

                        case "LastMonthReturns":
                            dataList = LMR(dataList);
                            break;

                        case "LastYearReturns":
                            dataList = LYR(dataList);
                            break;

                        case "LastQuarterReturns":
                            dataList = LQR(dataList);
                            break;

                        case "HistoricalCumulativeReturns":
                            dataList = HistoricalCumulativeReturn(dataList);
                            break;

                        case "AnnualReturns":
                            dataList = AnnualReturn(dataList);
                            break;

                        case "LastThreeMonthsReturns":
                            dataList = LastThreeMonthsReturn(dataList);
                            break;

                        default:
                            dataList.FundName = "Null Check the Inputs";
                            break;
                    }
                }
            }
            return dataList;
        }
    }
}
