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
    public class ApproverRolesController : ControllerBase
    {
        private readonly AgendaOrganizerContext _context;

        public ApproverRolesController(AgendaOrganizerContext context)
        {
            _context = context;
        }

        // GET: api/ApproverRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApproverRole>>> GetApproverRoles()
        {
          if (_context.ApproverRoles == null)
          {
              return NotFound();
          }
            return await _context.ApproverRoles.ToListAsync();
        }

        // GET: api/ApproverRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApproverRole>> GetApproverRole(int id)
        {
          if (_context.ApproverRoles == null)
          {
              return NotFound();
          }
            var approverRole = await _context.ApproverRoles.FindAsync(id);

            if (approverRole == null)
            {
                return NotFound();
            }

            return approverRole;
        }

        // PUT: api/ApproverRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApproverRole(int id, ApproverRole approverRole)
        {
            if (id != approverRole.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(approverRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApproverRoleExists(id))
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

        // POST: api/ApproverRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApproverRole>> PostApproverRole(ApproverRole approverRole)
        {
          if (_context.ApproverRoles == null)
          {
              return Problem("Entity set 'AgendaOrganizerContext.ApproverRoles'  is null.");
          }
            _context.ApproverRoles.Add(approverRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApproverRole", new { id = approverRole.RoleId }, approverRole);
        }

        // DELETE: api/ApproverRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApproverRole(int id)
        {
            if (_context.ApproverRoles == null)
            {
                return NotFound();
            }
            var approverRole = await _context.ApproverRoles.FindAsync(id);
            if (approverRole == null)
            {
                return NotFound();
            }

            _context.ApproverRoles.Remove(approverRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApproverRoleExists(int id)
        {
            return (_context.ApproverRoles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
