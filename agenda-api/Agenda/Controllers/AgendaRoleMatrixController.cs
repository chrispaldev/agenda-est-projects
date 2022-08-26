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
    public class AgendaRoleMatrixController : ControllerBase
    {
        private readonly AgendaOrganizerContext _context;

        public AgendaRoleMatrixController(AgendaOrganizerContext context)
        {
            _context = context;
        }

        // GET: api/AgendaRoleMatrix
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendaRoleMatrix>>> GetAgendaRoleMatrices()
        {
          if (_context.AgendaRoleMatrices == null)
          {
              return NotFound();
          }
            return await _context.AgendaRoleMatrices.ToListAsync();
        }

        // GET: api/AgendaRoleMatrix/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgendaRoleMatrix>> GetAgendaRoleMatrix(int id)
        {
          if (_context.AgendaRoleMatrices == null)
          {
              return NotFound();
          }
            var agendaRoleMatrix = await _context.AgendaRoleMatrices.FindAsync(id);

            if (agendaRoleMatrix == null)
            {
                return NotFound();
            }

            return agendaRoleMatrix;
        }

        // PUT: api/AgendaRoleMatrix/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendaRoleMatrix(int id, AgendaRoleMatrix agendaRoleMatrix)
        {
            if (id != agendaRoleMatrix.AgendaType)
            {
                return BadRequest();
            }

            _context.Entry(agendaRoleMatrix).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaRoleMatrixExists(id))
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

        // POST: api/AgendaRoleMatrix
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AgendaRoleMatrix>> PostAgendaRoleMatrix(AgendaRoleMatrix agendaRoleMatrix)
        {
          if (_context.AgendaRoleMatrices == null)
          {
              return Problem("Entity set 'AgendaOrganizerContext.AgendaRoleMatrices'  is null.");
          }
            _context.AgendaRoleMatrices.Add(agendaRoleMatrix);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AgendaRoleMatrixExists(agendaRoleMatrix.AgendaType))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAgendaRoleMatrix", new { id = agendaRoleMatrix.AgendaType }, agendaRoleMatrix);
        }

        // DELETE: api/AgendaRoleMatrix/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendaRoleMatrix(int id)
        {
            if (_context.AgendaRoleMatrices == null)
            {
                return NotFound();
            }
            var agendaRoleMatrix = await _context.AgendaRoleMatrices.FindAsync(id);
            if (agendaRoleMatrix == null)
            {
                return NotFound();
            }

            _context.AgendaRoleMatrices.Remove(agendaRoleMatrix);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgendaRoleMatrixExists(int id)
        {
            return (_context.AgendaRoleMatrices?.Any(e => e.AgendaType == id)).GetValueOrDefault();
        }
    }
}
