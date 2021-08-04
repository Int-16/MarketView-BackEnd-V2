using MarketView.Commons;
using MarketView.Engine;
using MarketView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MarketView.Services.Controllers
{
    [ApiController]
    public class DataService : ControllerBase
    {
        public IDataEngine Engine { get; }

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(DataService));

        readonly string TypeName;

        public DataService(IDataEngine engine)
        {
            this.Engine = engine;
            TypeName = this.GetType().Name;
        }

        /// <summary>
        /// Method to return all the MutualFund MetaData
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/[controller]/MutualFundData/")]
        public List<MutualFund> GetAllMutualFundData()
        {
            Logging.LogInfo(TypeName, "Control inside DataService GetAllMutualFundData");

            List<MutualFund> result;
            try
            {
                result = this.Engine.GetAllMutualFundData();
            }
            catch (Exception ex)
            {
                Logging.LogException(TypeName, ex.Message, ex);
                ApplicationException appEx = new ApplicationException("System error occurred.");
                throw appEx;
            }

            return result;
        }
    }
}
