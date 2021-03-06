using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTicket.api.Models
{
    public class ServiceTicket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int TicketNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public int Status { get; set; }
        public int CounterId { get; set; }
        public DateTime DateUpdated { get; set; }
        public string AssignedToUserId { get; set; }
    }
}
