using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using QTicket.api.Models;

namespace QTicket.api.Services
{
    public class TicketService
    {
        private readonly IMongoCollection<ServiceTicket> _serviceTicket;

        public TicketService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _serviceTicket = database.GetCollection<ServiceTicket>(settings.CollectionName);
        }

        public List<ServiceTicket> Get() => _serviceTicket.Find(serviceTicket => true).ToList();

        public ServiceTicket Get(string id) => _serviceTicket.Find<ServiceTicket>(serviceTicket => serviceTicket.Id == id).FirstOrDefault();

        public ServiceTicket Create(ServiceTicket serviceTicket)
        {
            _serviceTicket.InsertOne(serviceTicket);
            return serviceTicket;
        }

        public void Update(string id, ServiceTicket serviceTicketIn) => _serviceTicket.ReplaceOne(serviceTicket => serviceTicket.Id == id, serviceTicketIn);

        public void Remove(ServiceTicket serviceTicketIn) => _serviceTicket.DeleteOne(serviceTicket => serviceTicket.Id == serviceTicketIn.Id);

        public void Remove(string id) => _serviceTicket.DeleteOne(serviceTicket => serviceTicket.Id == id);

        public ServiceTicket GetNextServiceTicket()
        {
            ServiceTicket newTicket = new ServiceTicket();

            //var lastTicket = (from ticket in _serviceTicket.AsQueryable<ServiceTicket>()
            //                orderby ticket.TicketNumber 
            //                select ticket).FirstOrDefault();
            var lastTicket = _serviceTicket.AsQueryable<ServiceTicket>().OrderByDescending(x => x.TicketNumber).FirstOrDefault();


            if (lastTicket == null)
            {
                ServiceTicket ticket = new ServiceTicket();
                ticket.TicketNumber = 1;
                ticket.DateCreated = DateTime.Now;
                newTicket = Create(ticket);
            }
            else
            {
                ServiceTicket ticket = new ServiceTicket();
                ticket.TicketNumber = lastTicket.TicketNumber + 1;
                ticket.DateCreated = DateTime.Now;
                newTicket = Create(ticket);
            }

            return newTicket;
        }
    }
}
