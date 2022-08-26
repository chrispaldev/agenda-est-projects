using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Agenda.Models;

namespace Agenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaItemsController : ControllerBase
    {
        private readonly AgendaOrganizerContext _context;

        public AgendaItemsController(AgendaOrganizerContext context)
        {
            _context = context;
        }

        // GET: api/AgendaItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendaItem>>> GetAgendaItems()
        {
          if (_context.AgendaItems == null)
          {
              return NotFound();
          }
            return await _context.AgendaItems.ToListAsync();
        }

        // GET: api/AgendaItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgendaItem>> GetAgendaItem(int id)
        {
          if (_context.AgendaItems == null)
          {
              return NotFound();
          }
            var agendaItem = await _context.AgendaItems.FindAsync(id);

            if (agendaItem == null)
            {
                return NotFound();
            }

            return agendaItem;
        }

        // PUT: api/AgendaItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendaItem(int id, AgendaItem agendaItem)
        {
            if (id != agendaItem.AgendaId)
            {
                return BadRequest();
            }

            _context.Entry(agendaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaItemExists(id))
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

        // POST: api/AgendaItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AgendaItem>> PostAgendaItem(AgendaItem agendaItem)
        {
          if (_context.AgendaItems == null)
          {
              return Problem("Entity set 'AgendaOrganizerContext.AgendaItems'  is null.");
          }
            _context.AgendaItems.Add(agendaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgendaItem", new { id = agendaItem.AgendaId }, agendaItem);
        }

        // DELETE: api/AgendaItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendaItem(int id)
        {
            if (_context.AgendaItems == null)
            {
                return NotFound();
            }
            var agendaItem = await _context.AgendaItems.FindAsync(id);
            if (agendaItem == null)
            {
                return NotFound();
            }

            _context.AgendaItems.Remove(agendaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgendaItemExists(int id)
        {
            return (_context.AgendaItems?.Any(e => e.AgendaId == id)).GetValueOrDefault();
        }
    }
}
