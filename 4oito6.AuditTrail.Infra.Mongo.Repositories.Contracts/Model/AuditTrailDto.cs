using MongoDB.Bson.Serialization.Attributes;
using System;

namespace _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Model
{
    public sealed class AuditTrailDto
    {
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("stackTrace")]
        public string StackTrace { get; set; }
    }
}