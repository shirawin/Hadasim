using Repositories.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CoronaVirus
{
    public interface ICoronaVirusData
    {
        Task<string> addCoronaDedailes(CoronaVirusDetail coronaVirusDetail);

        Task<string> VerifiedForTheCoronaVirusList();


    }
}
