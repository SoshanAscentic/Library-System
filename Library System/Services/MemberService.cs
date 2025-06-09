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
        private int nextMemberId = 1;

        public MemberService(List<Member> members)
        {
            this.members = members ?? throw new ArgumentNullException(nameof(members));
        }

        public Member AddMember(string name, int memberType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            Member newMember = memberType switch
            {
                0 => new RegularMember(name, nextMemberId++),
                1 => new MinorStaff(name, nextMemberId++),
                2 => new ManagementStaff(name, nextMemberId++),
                _ => throw new ArgumentException("Invalid member type.")
            };

            members.Add(newMember);
            return newMember;
        }

        public void DisplayMembers()
        {
            if (members.Count == 0)
            {
                Console.WriteLine("No members registered in the library.");
                return;
            }
            foreach (var member in members)
            {
                Console.WriteLine($"Name: {member.Name}, ID: {member.MemberID}, Type: {member.GetMemberType()}, Borrowed Books: {member.BorrowedBooksCount}");
            }
        }

        IEnumerable<Member> IMemberService.GetAllMembers()
        {
            return members.AsReadOnly();
        }

        Member? IMemberService.GetMemberById(int memberId)
        {
            return members.FirstOrDefault(m => m.MemberID == memberId);
        }
    }
}
