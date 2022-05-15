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
    public class VacanciesController : ControllerBase
    {
        private readonly LaborExchangeDbContext _context;

        public VacanciesController(LaborExchangeDbContext context)
        {
            _context = context;
        }

        // GET: api/Vacancies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacancy>>> GetVacancies()
        {
            return await _context.Vacancies
                .Include(v => v.Profession)
                .Include(v => v.Education)
                .Include(v => v.Education.Rank)
                .Include(v => v.Education.Qualification)
                .Include(v => v.WorkDayRequirements)
                .Include(v => v.CompanyHasVacancies)
                .Where(v => !v.IsDeleted)
                .ToListAsync();
        }

        // GET: api/Vacancies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacancy>> GetVacancy(int id)
        {
            var vacancy = await _context.Vacancies
                .Include(v => v.Profession)
                .Include(v => v.Education)
                .Include(v => v.Education.Rank)
                .Include(v => v.Education.Qualification)
                .Include(v => v.WorkDayRequirements)
                .Include(v => v.CompanyHasVacancies)
                .Where(v => !v.IsDeleted)
                .FirstOrDefaultAsync(v => v.Id.Equals(id));

            if (vacancy == null)
            {
                return NotFound();
            }

            return vacancy;
        }

        // PUT: api/Vacancies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacancy(int id, Vacancy vacancy)
        {
            if (id != vacancy.Id)
            {
                return BadRequest();
            }

            _context.Entry(vacancy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacancyExists(id))
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

        // POST: api/Vacancies
        [HttpPost]
        public async Task<ActionResult<Vacancy>> PostVacancy(Vacancy vacancy)
        {
            _context.Vacancies.Add(vacancy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVacancy", new { id = vacancy.Id }, vacancy);
        }

        // DELETE: api/Vacancies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VacancyExists(int id)
        {
            return _context.Vacancies.Any(e => e.Id == id);
        }
    }
}