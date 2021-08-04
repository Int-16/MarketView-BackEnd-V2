using MongoDB.Bson.Serialization.Attributes;

namespace MarketView.Models
{
    public class PerDayStat
    {
        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("nav")]
        public double Nav { get; set; }

    }

}
