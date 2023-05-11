using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.GeneratedModels;
using Services.CoronaVirus;

namespace HMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoronaVirusDetailsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly ICoronaVirusData _dbStore;

        public CoronaVirusDetailsController(MyDBContext context, ICoronaVirusData dbStore)
        {
            _context = context;
            _dbStore = dbStore;
        }

        // GET: api/CoronaVirusDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoronaVirusDetail>>> GetCoronaVirusDetails()
        {
          if (_context.CoronaVirusDetails == null)
          {
              return NotFound();
          }
            return await _context.CoronaVirusDetails.ToListAsync();
        }

        // GET: api/CoronaVirusDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoronaVirusDetail>> GetCoronaVirusDetail(int id)
        {
          if (_context.CoronaVirusDetails == null)
          {
              return NotFound();
          }
            var coronaVirusDetail = await _context.CoronaVirusDetails.FindAsync(id);

            if (coronaVirusDetail == null)
            {
                return NotFound();
            }

            return coronaVirusDetail;
        }

        // PUT: api/CoronaVirusDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoronaVirusDetail(int id, CoronaVirusDetail coronaVirusDetail)
        {
            if (id != coronaVirusDetail.Code)
            {
                return BadRequest();
            }

            _context.Entry(coronaVirusDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoronaVirusDetailExists(id))
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

        // POST: api/CoronaVirusDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CoronaVirusDetail>> PostCoronaVirusDetail(CoronaVirusDetail coronaVirusDetail)
        {
          if (_context.CoronaVirusDetails == null)
          {
              return Problem("Entity set 'MyDBContext.CoronaVirusDetails'  is null.");
          }
            _context.CoronaVirusDetails.Add(coronaVirusDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoronaVirusDetail", new { id = coronaVirusDetail.Code }, coronaVirusDetail);
        }

        // DELETE: api/CoronaVirusDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoronaVirusDetail(int id)
        {
            if (_context.CoronaVirusDetails == null)
            {
                return NotFound();
            }
            var coronaVirusDetail = await _context.CoronaVirusDetails.FindAsync(id);
            if (coronaVirusDetail == null)
            {
                return NotFound();
            }

            _context.CoronaVirusDetails.Remove(coronaVirusDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("/api/addCoronaVirusDetails")]
        public async Task<ActionResult<IEnumerable<Member>>> createMember(CoronaVirusDetail coronaVirusDetail)
        {
            coronaVirusDetail.DateOfPositiveAnswer = coronaVirusDetail.DateOfPositiveAnswer.Value.ToLocalTime();
            coronaVirusDetail.DateOfRecovery = coronaVirusDetail.DateOfRecovery.Value.ToLocalTime();
            var result = await _dbStore.addCoronaDedailes(coronaVirusDetail);
            if (result== "Details added successfully")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("/api/chartData")]

        public async Task<string> GetActivePatientsLastMonth()
        {
            var result = await _dbStore.VerifiedForTheCoronaVirusList();


            return result;
        }
        private bool CoronaVirusDetailExists(int id)
        {
            return (_context.CoronaVirusDetails?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
