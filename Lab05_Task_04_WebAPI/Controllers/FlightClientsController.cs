using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightClientsController : ControllerBase
    {
        private readonly FlightContext _context;

        public FlightClientsController(FlightContext context)
        {
            _context = context;
        }

        // GET: api/FlightClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightClient>>> GetFlightClients()
        {
            return await _context.FlightClients.ToListAsync();
        }

        // GET: api/FlightClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightClient>> GetFlightClient(long id)
        {
            var flightClient = await _context.FlightClients.FindAsync(id);

            if (flightClient == null)
            {
                return NotFound();
            }

            return flightClient;
        }

        // PUT: api/FlightClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlightClient(long id, FlightClient flightClient)
        {
            if (id != flightClient.Id)
            {
                return BadRequest();
            }

            _context.Entry(flightClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FlightClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FlightClient>> PostFlightClient(FlightClient flightClient)
        {
            _context.FlightClients.Add(flightClient);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetFlightClient", new { id = flightClient.Id }, flightClient);
            
            // nameof operator
            return CreatedAtAction(nameof(GetFlightClient), new { id = flightClient.Id }, flightClient);
           
        }

        // DELETE: api/FlightClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlightClient(long id)
        {
            var flightClient = await _context.FlightClients.FindAsync(id);
            if (flightClient == null)
            {
                return NotFound();
            }

            _context.FlightClients.Remove(flightClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightClientExists(long id)
        {
            return _context.FlightClients.Any(e => e.Id == id);
        }
    }
}
