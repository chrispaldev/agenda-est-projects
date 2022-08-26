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
    public class AgendaTypesController : ControllerBase
    {
        private readonly AgendaOrganizerContext _context;

        public AgendaTypesController(AgendaOrganizerContext context)
        {
            _context = context;
        }

        // GET: api/AgendaTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendaType>>> GetAgendaTypes()
        {
          if (_context.AgendaTypes == null)
          {
              return NotFound();
          }
            return await _context.AgendaTypes.ToListAsync();
        }

        // GET: api/AgendaTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgendaType>> GetAgendaType(int id)
        {
          if (_context.AgendaTypes == null)
          {
              return NotFound();
          }
            var agendaType = await _context.AgendaTypes.FindAsync(id);

            if (agendaType == null)
            {
                return NotFound();
            }

            return agendaType;
        }

        // PUT: api/AgendaTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendaType(int id, AgendaType agendaType)
        {
            if (id != agendaType.AgendaTypeId)
            {
                return BadRequest();
            }

            _context.Entry(agendaType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaTypeExists(id))
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

        // POST: api/AgendaTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AgendaType>> PostAgendaType(AgendaType agendaType)
        {
          if (_context.AgendaTypes == null)
          {
              return Problem("Entity set 'AgendaOrganizerContext.AgendaTypes'  is null.");
          }
            _context.AgendaTypes.Add(agendaType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AgendaTypeExists(agendaType.AgendaTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAgendaType", new { id = agendaType.AgendaTypeId }, agendaType);
        }

        // DELETE: api/AgendaTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendaType(int id)
        {
            if (_context.AgendaTypes == null)
            {
                return NotFound();
            }
            var agendaType = await _context.AgendaTypes.FindAsync(id);
            if (agendaType == null)
            {
                return NotFound();
            }

            _context.AgendaTypes.Remove(agendaType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgendaTypeExists(int id)
        {
            return (_context.AgendaTypes?.Any(e => e.AgendaTypeId == id)).GetValueOrDefault();
        }
    }
}
