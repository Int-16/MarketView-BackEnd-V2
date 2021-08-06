using MarketView.Commons;
using MarketView.Engine;
using MarketView.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketView.Services.Controllers
{
    [ApiController]
    public class CalculationService : ControllerBase
    {
        public ICalculationEngine Engine { get; }

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(DataService));

        readonly string TypeName;

        public CalculationService(ICalculationEngine engine)
        {
            this.Engine = engine;
            TypeName = this.GetType().Name;
        }

        [HttpGet("api/[controller]/fetch/{fundId}")]
        public List<MutualFund> GetFundById(string fundId)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationService GetFundById");

            List<MutualFund> result;

            try
            {
                result = this.Engine.GetFundById(fundId);
            }
            catch (Exception ex)
            {
                Logging.LogException(TypeName, ex.Message, ex);
                ApplicationException appEx = new ApplicationException("System error occurred.");
                throw appEx;
            }

            return result;
        }

        [HttpGet("api/[controller]")]
        public FundTimeSeries ReturnsCalculator([FromQuery] string fundId, [FromQuery] string statName)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationService ReturnsCalculator");

            string[] statNames = statName.Split(',');

            FundTimeSeries result;
            try
            {
                result = this.Engine.ReturnsCalculator(fundId, statNames.ToList());
            }
            catch (Exception ex)
            {
                Logging.LogException(TypeName, ex.Message, ex);
                ApplicationException appEx = new ApplicationException("System error occurred.");
                //throw appEx;
                throw ex;
            }
            return result;
        }

        [HttpGet("api/[controller]/monthlytimeseries")]
        public FundYearlySeries MonthlyTimeSeriesCalculator([FromQuery] string fundId)
        {
            Logging.LogInfo(TypeName, "Control inside CalculationService MonthlyTimeSeriesCalculator");

            FundYearlySeries result;
            try
            {
                result = this.Engine.MonthlyTimeSeries(fundId);
            }
            catch (Exception ex)
            {
                Logging.LogException(TypeName, ex.Message, ex);
                ApplicationException appEx = new ApplicationException("System error occurred.");
                //throw appEx;
                throw ex;
            }
            return result;
        }


    }
}
