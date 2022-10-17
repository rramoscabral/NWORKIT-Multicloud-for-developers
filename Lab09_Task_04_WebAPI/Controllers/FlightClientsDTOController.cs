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
    public class FlightClientsDTOController : ControllerBase
    {
        private readonly FlightContext _context;

        private static FlightClientDTO FlightClientItemDTO(FlightClient  flightClient) =>
            new FlightClientDTO
            {
                Id = flightClient.Id,
                Name = flightClient.Name,
                BirthDate = flightClient.BirthDate,
                Nationality = flightClient.Nationality,
                PassaportNumber = flightClient.PassaportNumber,
                PhotoBase64 = flightClient.PhotoBase64
            };

        public FlightClientsDTOController(FlightContext context)
        {
            _context = context;
        }

        // GET: api/FlightClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightClientDTO>>> GetFlightClients()
        {
            return await _context.FlightClients
                .Select(x => FlightClientItemDTO(x))
                .ToListAsync();
        }

        // GET: api/FlightClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightClientDTO>> GetFlightClient(long id)
        {
            var flightClient = await _context.FlightClients.FindAsync(id);

            if (flightClient == null)
            {
                return NotFound();
            }

            return FlightClientItemDTO(flightClient);
        }

        // PUT: api/FlightClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlightClient(long id, FlightClientDTO flightClientDTO)
        {
            if (id != flightClientDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(flightClientDTO).State = EntityState.Modified;

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
        public async Task<ActionResult<FlightClientDTO>> PostFlightClient(FlightClientDTO flightClientDTO)
        {

             var flightClient = new FlightClient
            {
                Name = flightClientDTO.Name,
                BirthDate = flightClientDTO.BirthDate,
                Nationality = flightClientDTO.Nationality,
                PassaportNumber = flightClientDTO.PassaportNumber,
                PhotoBase64 = flightClientDTO.PhotoBase64
            };


            _context.FlightClients.Add(flightClient);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetFlightClient", new { id = flightClient.Id }, flightClient);
            
            // nameof operator
            return CreatedAtAction(nameof(GetFlightClient), new { id = flightClientDTO.Id }, flightClientDTO);
           
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
