using Repositories.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Vaccinations
{
    public class VaccinationData : IVaccinationData
    {
        private readonly MyDBContext _context;
        public VaccinationData(MyDBContext context)
        {
            _context = context;
        }

        public async Task<string> addVaccination(Vaccination vaccination)
        {
            var isOk = true;
            int numVaccinations = _context.Vaccinations.Count(v => v.MemberCode == vaccination.MemberCode);
            if(numVaccinations < 4)
            {
                if (vaccination.DateReceived > DateTime.Now) return "Invalid Date";
                await _context.AddAsync(vaccination);
                isOk = await _context.SaveChangesAsync() >= 0;
                if (isOk) return "Vaccination details added successfully";
            }

            return "Exceeding the vaccination quota";
        }
    }
}
