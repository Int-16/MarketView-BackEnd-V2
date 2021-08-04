
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MarketView.Models
{
    public class MutualFund
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("meta")]
        public MetaData MetaData { get; set; }

        [BsonElement("data")]
        public List<PerDayStat> TimeSeries { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
    }

}
