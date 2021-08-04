using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MarketView.Models
{
    public class MetaData
    {
        [BsonElement("fund_house")]
        public string FundHouse { get; set; }

        [BsonElement("scheme_type")]
        public string SchemeType { get; set; }

        [BsonElement("scheme_category")]
        public string SchemeCategory { get; set; }

        
        [BsonElement("scheme_code")]
        public int SchemeCode { get; set; }

        [BsonElement("scheme_name")]
        public string SchemeName { get; set; }

        public static implicit operator List<object>(MetaData v)
        {
            throw new NotImplementedException();
        }
    }

}
