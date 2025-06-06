using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    public abstract class Member
    {
        public string Name { get; set; }
        public int MemberID { get; private set; }
        public int BorrowedBooksCount { get; set; } = 0;

        public Member(string name, int memberId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            if (memberId <= 0)
            {
                throw new ArgumentException("Member ID must be a positive integer.", nameof(memberId));
            }
            Name = name;
            MemberID = memberId;
        }

        public abstract string GetMemberType();

        public virtual bool CanBorrowBooks() => false;
        public virtual bool CanViewBooks() => false;
        public virtual bool CanViewMembers() => false;
        public virtual bool CanAddRemoveBooks() => false;
    }
}
