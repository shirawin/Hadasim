using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class MemberDTO
    {
        public int Code { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Id { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? HouseNumber { get; set; }

        public string? Phone { get; set; }

        public string? MobilePhone { get; set; }
    }
}
