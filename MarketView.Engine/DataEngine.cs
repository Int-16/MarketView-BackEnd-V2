using MarketView.Commons;
using MarketView.Models;
using System;
using System.Collections.Generic;

namespace MarketView.Engine
{
    public class DataEngine : IDataEngine
    {
        public Data.IMutualFundLibrary MutualFundLibrary { get; set; }

        readonly string TypeName;

        public DataEngine(Data.IMutualFundLibrary mutualFundLibrary)
        {
            this.MutualFundLibrary = mutualFundLibrary;
            TypeName = this.GetType().Name;
        }

        /// <summary>
        ///DataService
        ///Returns all the MutualFund MetaData
        /// </summary>
        /// <returns></returns>
        public List<MutualFund> GetAllMutualFundData()
        {
            Logging.LogInfo(TypeName, "Control inside DataEngine GetAllMutualFundData");

            List<MutualFund> resultList = this.MutualFundLibrary.GetAllMutualFundData();
            return resultList;
        }
    }
}
