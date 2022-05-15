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
    public class FamilyStatusesController : ControllerBase
    {
        private readonly LaborExchangeDbContext _context;

        public FamilyStatusesController(LaborExchangeDbContext context)
        {
            _context = context;
        }

        // GET: api/FamilyStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyStatus>>> GetFamilyStatuses()
        {
            return await _context.FamilyStatuses.Where(f => !f.IsDeleted).ToListAsync();
        }

        // GET: api/FamilyStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyStatus>> GetFamilyStatus(int id)
        {
            var familyStatus = await _context.FamilyStatuses.Where(f => !f.IsDeleted).FirstOrDefaultAsync(f => f.Id.Equals(id));

            if (familyStatus == null)
            {
                return NotFound();
            }

            return familyStatus;
        }

        // PUT: api/FamilyStatuses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamilyStatus(int id, FamilyStatus familyStatus)
        {
            if (id != familyStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(familyStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyStatusExists(id))
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

        // POST: api/FamilyStatuses
        [HttpPost]
        public async Task<ActionResult<FamilyStatus>> PostFamilyStatus(FamilyStatus familyStatus)
        {
            _context.FamilyStatuses.Add(familyStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFamilyStatus", new { id = familyStatus.Id }, familyStatus);
        }

        // DELETE: api/FamilyStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamilyStatus(int id)
        {
            var familyStatus = await _context.FamilyStatuses.FindAsync(id);
            if (familyStatus == null)
            {
                return NotFound();
            }

            _context.FamilyStatuses.Remove(familyStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FamilyStatusExists(int id)
        {
            return _context.FamilyStatuses.Any(e => e.Id == id);
        }
    }
}