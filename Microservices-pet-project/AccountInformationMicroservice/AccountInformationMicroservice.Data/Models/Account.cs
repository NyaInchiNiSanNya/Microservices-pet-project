using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AccountInformationMicroservice.DbData
{
    public class Account
    {
        [BsonId]
        [BsonElement("name")]
        public String AccountName { get; set; }

        [BsonElement("balance")]
        public Decimal Balance  { get; set; }
    }
}