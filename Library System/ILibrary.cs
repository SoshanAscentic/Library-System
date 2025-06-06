using Library_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{

    public interface ILibrary
    {
        void AddBook(Book book);
        Member AddMember(string name, int memberType);
        void RemoveBook(string title, int publicationYear);
        void BorrowBook(string title, int publicationYear, int memberId);
        void ReturnBook(string title, int publicationYear, int memberId);
        void DisplayBooks();
        void DisplayMembers();
        Member? GetMemberById(int memberId); 
    }
}
