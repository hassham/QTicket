using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTicket.api.Services;
using Microsoft.AspNetCore.Cors;
using QTicket.api.Entities;

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
        public async Task<ActionResult<List<ServiceTicket>>> Get() =>
            await _ticketService.Get();

        [HttpGet]
        [Route("GetUnassignedTickets")]
        public async Task<ActionResult<List<ServiceTicket>>> GetUnassignedTickets()
        {
            return await _ticketService.GetUnassignedTickets();
        }

        [HttpGet("{id:length(24)}", Name = "GetTicket")]
        public async Task<ActionResult<ServiceTicket>> Get(string id)
        {
            var ticket = await _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceTicket>> Create(ServiceTicket ticket)
        {
            await _ticketService.Create(ticket);

            return CreatedAtRoute("GetTicket", new { id = ticket.Id.ToString() }, ticket);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ServiceTicket ticketIn)
        {
            var ticket = await _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            await _ticketService.Update(id, ticketIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ticket = await _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            await _ticketService.Remove(ticket.Id);

            return NoContent();
        }

        [HttpGet]
        [Route("GetNextTicket")]
        public async Task<ActionResult<ServiceTicket>> GetNextTicket()
        {
            return await _ticketService.GetNextServiceTicket();
        }
    }
}
