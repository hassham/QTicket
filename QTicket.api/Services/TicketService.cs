using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using QTicket.api.Entities;

namespace QTicket.api.Services
{
    public class TicketService
    {
        private readonly IMongoCollection<ServiceTicket> _serviceTicket;

        public TicketService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _serviceTicket = database.GetCollection<ServiceTicket>("ServiceTickets");
        }

        public async Task<List<ServiceTicket>> Get() => await _serviceTicket.Find(serviceTicket => true).ToListAsync();

        public async Task<ServiceTicket> Get(string id) => await _serviceTicket.Find<ServiceTicket>(serviceTicket => serviceTicket.Id == id).FirstOrDefaultAsync();


        public async Task<ServiceTicket> Create(ServiceTicket serviceTicket)
        {
            await _serviceTicket.InsertOneAsync(serviceTicket);
            return serviceTicket;
        }

        public async Task Update(string id, ServiceTicket serviceTicketIn) => await _serviceTicket.ReplaceOneAsync(serviceTicket => serviceTicket.Id == id, serviceTicketIn);

        public async Task Remove(ServiceTicket serviceTicketIn) => await _serviceTicket.DeleteOneAsync(serviceTicket => serviceTicket.Id == serviceTicketIn.Id);

        public async Task Remove(string id) => await _serviceTicket.DeleteOneAsync(serviceTicket => serviceTicket.Id == id);

        public async Task <List<ServiceTicket>> GetUnassignedTickets()
        {
            return await _serviceTicket.Find<ServiceTicket>(x => x.Status == 1 && string.IsNullOrEmpty(x.AssignedToUserId)).ToListAsync();
        }

        public async Task<ServiceTicket> GetNextServiceTicket()
        {
            ServiceTicket newTicket = new ServiceTicket();

            var lastTicket = await _serviceTicket.AsQueryable<ServiceTicket>().OrderByDescending(x => x.TicketNumber).FirstOrDefaultAsync();


            if (lastTicket == null)
            {
                ServiceTicket ticket = new ServiceTicket();
                ticket.TicketNumber = 1;
                ticket.DateCreated = DateTime.Now;
                ticket.Status = 1;
                newTicket = await Create(ticket);
            }
            else
            {
                ServiceTicket ticket = new ServiceTicket();
                ticket.TicketNumber = lastTicket.TicketNumber + 1;
                ticket.DateCreated = DateTime.Now;
                ticket.Status = 1;
                newTicket = await Create(ticket);
            }

            return newTicket;
        }
    }
}
