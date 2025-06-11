using Library_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Interfaces
{
    public interface IBookService
    {
        void AddBook(Book book);
        void RemoveBook(string title, int publicationYear);
        void DisplayBooks();
        Book? GetBook(string title, int publicationYear);
        IEnumerable<Book> GetAllBooks();
    }
}
