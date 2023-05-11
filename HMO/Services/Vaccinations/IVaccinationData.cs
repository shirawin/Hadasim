using Repositories.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Vaccinations
{
    public interface IVaccinationData
    {
        Task <string> addVaccination(Vaccination vaccination);
    }
}
