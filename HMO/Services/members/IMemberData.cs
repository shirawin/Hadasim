using Repositories.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DTO;

namespace Services.members
{
    public interface IMemberData
    {
        Task<string> createMember(Member member);
        Task<int> isExsitsMember(string id);
        //string validateCheck(Member member);
        //bool IsValidIdNumber(string idNumber);
        //bool IsValidPhoneNumber(string phoneNumber);
    }
}
