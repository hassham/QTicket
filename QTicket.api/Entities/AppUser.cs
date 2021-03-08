using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace QTicket.api.Entities
{
    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
