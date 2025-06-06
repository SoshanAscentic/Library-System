using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    public class Member
    {
        public enum MemberType
        {
            Member,
            Staff,
            Management
        }

        public string Name { get; set; }
        public int MemberID { get; private set; }
        public MemberType Type { get; set; }

        public int BorrowedBooksCount { get; set; } = 0;

        public Member(string name, int memberId, MemberType type)
        {
            Name = name;
            MemberID = memberId;
            Type = type;
        }



    }
}
