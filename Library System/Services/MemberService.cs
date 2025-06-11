using Library_System.Interfaces;
using Library_System.Models.Members;
using Library_System.Models.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Services
{
    public class MemberService : IMemberService
    {
        private readonly List<Member> members;
        private readonly IConsoleService console;
        private int nextMemberId = 1;

        public MemberService(List<Member> members, IConsoleService console)
        {
            this.members = members ?? throw new ArgumentNullException(nameof(members));
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }

        public Member AddMember(string name, int memberType)
        {
            Member newMember = CreateMember(name, memberType, nextMemberId++);
            members.Add(newMember);
            return newMember;
        }

        private static Member CreateMember(string name, int memberType, int memberId)
        {
            return memberType switch
            {
                0 => new RegularMember(name, memberId),
                1 => new MinorStaff(name, memberId),
                2 => new ManagementStaff(name, memberId),
                _ => throw new ArgumentException("Invalid member type.")
            };
        }

        public void DisplayMembers()
        {
            if (!members.Any())
            {
                console.WriteLine("No members registered in the library.");
                return;
            }

            console.WriteLine("\n=== Library Members ===");
            foreach (var member in members)
            {
                console.WriteLine(member.ToString());
            }
        }

        public Member? GetMemberById(int memberId)
        {
            return members.FirstOrDefault(m => m.MemberID == memberId);
        }

        public IEnumerable<Member> GetAllMembers() => members.AsReadOnly();
    }
}
