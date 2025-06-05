using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    internal class Member
    {
        public enum MemberType
        {
            Member,
            Staff,
            Management
        }

        public required string Name { get; set; }
        public int MemberID { get; set; }
        public MemberType Type { get; set; }

        public int BorrowedBooksCount { get; private set; } = 0;

        public Member(string name, int memberId, MemberType type)
        {
            Name = name;
            MemberID = memberId;
            Type = type;
        }



    }
}
