using Repositories.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DTO;
using System.Text.RegularExpressions;

namespace Services.members
{
    public class MemberData : IMemberData
    {
        private readonly MyDBContext _context;
        public MemberData(MyDBContext context)
        {
            _context = context;
        }

        public Task<int> isExsitsMember(string id)
        {
            var checkID =  _context.Members.Where(m => m.Id == id).FirstOrDefault();
            if (checkID != null)
            {
                return Task.FromResult(checkID.Code);
            }
            return Task.FromResult(-1);
        }
        public async Task<string> createMember(Member member)
        {
            var res = validateCheck(member);
            if (res == "Correct")
            {

                var isExsistsMember = await isExsitsMember(member.Id);
                var isOk = false;

                if (isExsistsMember == -1)
                {
                    await _context.AddAsync(member);
                    isOk = await _context.SaveChangesAsync() >= 0;
                    if (isOk) return "New member successfully added!";
                }
                return "Member exists in the system!";
            }
            return res;

         
        }

        public string validateCheck(Member member)
        {
            //ID
            if (!IsValidIdNumber(member.Id)) return "Invalid ID number";
            //MobilePhone
            if (!IsValidIsraeliMobileNumber(member.MobilePhone)) return "Invalid MobilePhone number";
            //DateOfBirth
            if (!IsValidDateOfBirth((DateTime)member.DateOfBirth)) return "Invalid Date Of Birth";
            //FirstName
            if (!IsValidName(member.FirstName)) return "Invalid name";
            //LastName
            if (!IsValidName(member.LastName)) return "Invalid LastName";
            return "Correct";
        }


        public bool IsValidIdNumber(string idNumber)
        {
            string regexPattern = @"^\d{9}$";

            return Regex.IsMatch(idNumber, regexPattern);
        }
        public bool IsValidIsraeliMobileNumber(string phoneNumber)
        {
            // ביטוי רגולרי לבדיקת מספר טלפון נייד ישראלי
            string regexPattern = @"^05\d{8}$";

            // בדיקה אם מספר הטלפון מתאים לביטוי הרגולרי
            return Regex.IsMatch(phoneNumber, regexPattern);
        }

        public bool IsValidDateOfBirth(DateTime dateOfBirth)
        {
            DateTime lowerLimit = DateTime.Now.AddYears(-120);
            if (dateOfBirth < lowerLimit)
            {
                return false;
            }
            DateTime upperLimit = DateTime.Now;
            if (dateOfBirth > upperLimit)
            {
                return false;
            }

            return true;
        }
        public bool IsValidName(string name)
        {
            // ביטוי רגולרי לבדיקת שם תקין (רק אותיות)
            string regexPattern = @"^[a-zA-Z]+$";

            // בדיקה אם השם מתאים לביטוי הרגולרי
            return Regex.IsMatch(name, regexPattern);
        }


    }
}
