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
        void add_book(Book book);
        void add_member(Member member);
        void remove_book(string title, int publicationYear);
        void borrow_book(string title, int publicationYear, int memberId);
        void return_book(string title, int publicationYear, int memberId);
        void display_books();
        void display_members();
    }
}
