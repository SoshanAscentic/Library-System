using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Interfaces
{
    public interface IMemberService
    {
        Member AddMember(string name, int memberType);
        void DisplayMembers();
        Member? GetMemberById(int memberId);
        IEnumerable<Member> GetAllMembers();
    }

}
