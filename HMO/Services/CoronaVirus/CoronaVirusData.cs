using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.GeneratedModels;

namespace Services.CoronaVirus
{
    public class CoronaVirusData : ICoronaVirusData
    {
        private readonly MyDBContext _context;
        public CoronaVirusData(MyDBContext context)
        {
            _context = context;
        }

        public async Task<string> addCoronaDedailes(CoronaVirusDetail coronaVirusDetail)
        {
            var isOk=false;
            var res = validateDates((DateTime)coronaVirusDetail.DateOfPositiveAnswer, (DateTime)coronaVirusDetail.DateOfRecovery);
            if (res == "OK")
            {
                await _context.AddAsync(coronaVirusDetail);
                isOk = await _context.SaveChangesAsync() >= 0;
            }
            if (isOk) return "Details added successfully";
            return res; ;
        }

        private string validateDates(DateTime dateOfPositiveAnswer, DateTime dateOfRecovery)
        {

            if (dateOfPositiveAnswer > dateOfRecovery) return "Invalid date range";

            if (dateOfPositiveAnswer > DateTime.Now) return "Invalid positive answer date";

            return "OK";

        }

        public async Task<string> VerifiedForTheCoronaVirusList()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-30);

            var patients = await _context.CoronaVirusDetails.ToListAsync();

            var activePatientsByDate = new List<object>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var activePatients = patients
                    .Count(c => c.DateOfPositiveAnswer <= date && (!c.DateOfRecovery.HasValue || c.DateOfRecovery.Value >= date));

                activePatientsByDate.Add(new { Date = date.Date, ActivePatients = activePatients });
            }

            var json = JsonConvert.SerializeObject(activePatientsByDate);

            return json;
        }

    }
}
