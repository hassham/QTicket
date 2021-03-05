using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTicket.api.Models;
using QTicket.api.Services;

namespace QTicket.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketsController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public ActionResult<List<ServiceTicket>> Get() =>
            _ticketService.Get();

        [HttpGet("{id:length(24)}", Name = "GetTicket")]
        public ActionResult<ServiceTicket> Get(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpPost]
        public ActionResult<ServiceTicket> Create(ServiceTicket ticket)
        {
            _ticketService.Create(ticket);

            return CreatedAtRoute("GetTicket", new { id = ticket.Id.ToString() }, ticket);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ServiceTicket ticketIn)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _ticketService.Update(id, ticketIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _ticketService.Remove(ticket.Id);

            return NoContent();
        }

        [HttpGet]
        [Route("GetNextTicket")]
        public ActionResult<ServiceTicket> GetNextTicket()
        {
            var ticket = _ticketService.GetNextServiceTicket();

            return ticket;
        }
    }
}
