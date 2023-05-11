using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.GeneratedModels;
using Services.Vaccinations;

namespace HMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IVaccinationData _dbStore;

        public VaccinationsController(MyDBContext context, IVaccinationData dbStore)
        {
            _context = context;
            _dbStore = dbStore;
        }

        // GET: api/Vaccinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaccination>>> GetVaccinations()
        {
          if (_context.Vaccinations == null)
          {
              return NotFound();
          }
            return await _context.Vaccinations.ToListAsync();
        }

        // GET: api/Vaccinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaccination>> GetVaccination(int id)
        {
          if (_context.Vaccinations == null)
          {
              return NotFound();
          }
            var vaccination = await _context.Vaccinations.FindAsync(id);

            if (vaccination == null)
            {
                return NotFound();
            }

            return vaccination;
        }

        // PUT: api/Vaccinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaccination(int id, Vaccination vaccination)
        {
            if (id != vaccination.VaccinationCode)
            {
                return BadRequest();
            }

            _context.Entry(vaccination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinationExists(id))
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

        // POST: api/Vaccinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vaccination>> PostVaccination(Vaccination vaccination)
        {
          if (_context.Vaccinations == null)
          {
              return Problem("Entity set 'MyDBContext.Vaccinations'  is null.");
          }
            _context.Vaccinations.Add(vaccination);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaccination", new { id = vaccination.VaccinationCode }, vaccination);
        }

        // DELETE: api/Vaccinations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccination(int id)
        {
            if (_context.Vaccinations == null)
            {
                return NotFound();
            }
            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }

            _context.Vaccinations.Remove(vaccination);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost]
        [Route("/api/addVaccination")]
        public async Task<ActionResult<IEnumerable<Member>>> addVaccination(Vaccination vaccination)
        {
            vaccination.DateReceived = vaccination.DateReceived.Value.ToLocalTime(); 
            var result = await _dbStore.addVaccination(vaccination);
            if (result== "Vaccination details added successfully")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        private bool VaccinationExists(int id)
        {
            return (_context.Vaccinations?.Any(e => e.VaccinationCode == id)).GetValueOrDefault();
        }
    }
}
