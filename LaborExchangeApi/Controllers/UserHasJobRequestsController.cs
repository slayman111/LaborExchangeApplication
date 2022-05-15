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
    public class UserHasJobRequestsController : ControllerBase
    {
        private readonly LaborExchangeDbContext _context;

        public UserHasJobRequestsController(LaborExchangeDbContext context)
        {
            _context = context;
        }

        // GET: api/UserHasJobRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserHasJobRequest>>> GetUserHasJobRequests()
        {
            return await _context.UserHasJobRequests.Where(uj => !uj.IsDeleted).ToListAsync();
        }

        // GET: api/UserHasJobRequests/5/5
        [HttpGet("{userId}/{jobRequestId}")]
        public async Task<ActionResult<UserHasJobRequest>> GetUserHasJobRequest(int userId, int jobRequestId)
        {
            var userHasJobRequest = await _context.UserHasJobRequests
                .Where(uj => !uj.IsDeleted)
                .FirstOrDefaultAsync(uj => uj.UserId.Equals(userId) && uj.JobRequestId.Equals(jobRequestId));

            if (userHasJobRequest == null)
            {
                return NotFound();
            }

            return userHasJobRequest;
        }

        // PUT: api/UserHasJobRequests/5/5
        [HttpPut("{userId}/{jobRequestId}")]
        public async Task<IActionResult> PutUserHasJobRequest(int userId, int jobRequestId, UserHasJobRequest userHasJobRequest)
        {
            if (userId != userHasJobRequest.UserId || jobRequestId != userHasJobRequest.JobRequestId)
            {
                return BadRequest();
            }

            _context.Entry(userHasJobRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserHasJobRequestExists(userId, jobRequestId))
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

        // POST: api/UserHasJobRequests
        [HttpPost]
        public async Task<ActionResult<UserHasJobRequest>> PostUserHasJobRequest(UserHasJobRequest userHasJobRequest)
        {
            _context.UserHasJobRequests.Add(userHasJobRequest);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserHasJobRequestExists(userHasJobRequest.UserId, userHasJobRequest.JobRequestId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserHasJobRequest", new { userId = userHasJobRequest.UserId, jobRequestId = userHasJobRequest.JobRequestId },
                userHasJobRequest);
        }

        // DELETE: api/UserHasJobRequests/5/5
        [HttpDelete("{userId}/{jobRequestId}")]
        public async Task<IActionResult> DeleteUserHasJobRequest(int userId, int jobRequestId)
        {
            var userHasJobRequest = await _context.UserHasJobRequests.FindAsync(userId, jobRequestId);
            if (userHasJobRequest == null)
            {
                return NotFound();
            }

            _context.UserHasJobRequests.Remove(userHasJobRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserHasJobRequestExists(int userId, int jobRequestId)
        {
            return _context.UserHasJobRequests.Any(e => e.UserId == userId && e.JobRequestId == jobRequestId);
        }
    }
}