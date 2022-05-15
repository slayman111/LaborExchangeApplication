using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaborExchangeApi.Models;

namespace LaborExchangeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDayRequirementsController : ControllerBase
    {
        private readonly LaborExchangeDbContext _context;

        public WorkDayRequirementsController(LaborExchangeDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkDayRequirements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDayRequirement>>> GetWorkDayRequirements()
        {
            return await _context.WorkDayRequirements.Where(w => !w.IsDeleted).ToListAsync();
        }

        // GET: api/WorkDayRequirements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkDayRequirement>> GetWorkDayRequirement(int id)
        {
            var workDayRequirement = await _context.WorkDayRequirements.Where(w => !w.IsDeleted).FirstOrDefaultAsync(w => w.Id.Equals(id));

            if (workDayRequirement == null)
            {
                return NotFound();
            }

            return workDayRequirement;
        }

        // PUT: api/WorkDayRequirements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkDayRequirement(int id, WorkDayRequirement workDayRequirement)
        {
            if (id != workDayRequirement.Id)
            {
                return BadRequest();
            }

            _context.Entry(workDayRequirement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkDayRequirementExists(id))
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

        // POST: api/WorkDayRequirements
        [HttpPost]
        public async Task<ActionResult<WorkDayRequirement>> PostWorkDayRequirement(WorkDayRequirement workDayRequirement)
        {
            _context.WorkDayRequirements.Add(workDayRequirement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkDayRequirement", new { id = workDayRequirement.Id }, workDayRequirement);
        }

        // DELETE: api/WorkDayRequirements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkDayRequirement(int id)
        {
            var workDayRequirement = await _context.WorkDayRequirements.FindAsync(id);
            if (workDayRequirement == null)
            {
                return NotFound();
            }

            _context.WorkDayRequirements.Remove(workDayRequirement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkDayRequirementExists(int id)
        {
            return _context.WorkDayRequirements.Any(e => e.Id == id);
        }
    }
}