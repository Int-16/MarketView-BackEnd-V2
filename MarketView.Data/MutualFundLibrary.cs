using MarketView.Commons;
using MarketView.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MarketView.Data
{
    public class MutualFundLibrary : IMutualFundLibrary
    {
        readonly string TypeName;
        public IMongoHandler MongoHandler { get; set; }

        public IMongoCollection<MutualFund> _mutualFundCollection;

        public IMongoCollection<MutualFund> MutualFundCollection
        {

            get
            {
                if (_mutualFundCollection == null)
                {
                    var collectionName = MongoConfigurations.GetCollectionName<MutualFund>();

                    _mutualFundCollection = MongoHandler.Db.GetCollection<MutualFund>(collectionName);
                }
                return _mutualFundCollection;
            }
        }

        /*public static DateTime IsDateTime(string txtDate)
        {
            DateTime tempDate;

            if (DateTime.TryParseExact(txtDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
            {
                System.Diagnostics.Debug.WriteLine("Here");
                return tempDate;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Here   dfvdf");
                return tempDate;
            }
        }*/

        public MutualFundLibrary(IMongoHandler dataHandler)
        {
            this.MongoHandler = dataHandler;
            TypeName = this.GetType().Name;
        }

        /// <summary>
        /// From DataService to return all the MutualFund MetaData
        /// </summary>
        /// <returns>MutualFund MetaData as a List</returns>
        public List<MutualFund> GetAllMutualFundData()
        {
            Logging.LogInfo(TypeName, "Control inside MutualFundLibrary GetAllMutualFundData");

            var builder = Builders<MutualFund>.Filter;
            var query = MutualFundCollection.Find(_ => true).Project<MutualFund>(Builders<MutualFund>.Projection.Include(p => p.MetaData));
            var list = query.ToList();
            return list;
        }

        /// <summary>
        /// From Calculation Service to return the MutualFund of Specific MutualFund Id
        /// </summary>
        /// <param name="fundId"></param>
        /// <returns>MutualFund Data as a List</returns>
        public List<MutualFund> GetFundById(string fundId)
        {
            Logging.LogInfo(TypeName, "Control inside MutualFundLibrary GetFundById");

            var builder = Builders<MutualFund>.Filter;

            var filter = builder.Where(x => x.Id == fundId);
            var cursor = MutualFundCollection.Find(filter).Sort(Builders<MutualFund>.Sort.Descending("TimeSeries"));
            return cursor.ToList();
        }

        /// <summary>
        /// From Calculation Service to return the MutualFund TimeSeries Data 
        /// </summary>
        /// <param name="fundId"></param>
        /// <returns>MutualFund TimeSeries Data</returns>
        public FundTimeSeries ReturnsCalculator(string fundId)
        {
            Logging.LogInfo(TypeName, "Control inside MutualFundLibrary ReturnCalculator");

            var builder = Builders<MutualFund>.Filter;
            var filter = builder.Where(x => x.Id == fundId);

            var cursor = MutualFundCollection.Find(filter).ToListAsync();            
            var dataList = cursor.Result;

            FundTimeSeries resultList =  new FundTimeSeries();

            var fundTimeSeries = new FundTimeSeries();

            foreach (var item in dataList)
            {
                fundTimeSeries = new FundTimeSeries()
                {
                    FundName = item.MetaData.FundHouse,
                    SchemeType = item.MetaData.SchemeType,
                    SchemeCategory = item.MetaData.SchemeCategory,
                    SchemeName = item.MetaData.SchemeName,
                    TimeSeries = new Dictionary<string, StatDictionary>()
                };
                foreach (var i in item.TimeSeries)
                {
                    StatDictionary stat = new StatDictionary();
                    stat.Stats.Add("nav", i.Nav);
                    DateTime dateTime = new DateTime(); 
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(i.Date, "dd-MM-yyyy", null));
                    string inp = dateTime.ToString("yyyy-MM-dd");
                    stat.Date = inp;
                    fundTimeSeries.TimeSeries.Add(inp, stat);
                }
            }
            return fundTimeSeries;
        }
    }
}
