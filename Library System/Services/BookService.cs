using Library_System.Interfaces;
using Library_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> books;
        private readonly IConsoleService console;

        public BookService(List<Book> books, IConsoleService console)
        {
            this.books = books ?? throw new ArgumentNullException(nameof(books));
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }

        public void AddBook(Book book)
        {
            books.Add(book ?? throw new ArgumentNullException(nameof(book)));
        }

        public void RemoveBook(string title, int publicationYear)
        {
            var book = GetBook(title, publicationYear);
            if (book != null)
            {
                books.Remove(book);
            }
            else
            {
                throw new InvalidOperationException("Book not found.");
            }
        }

        public void DisplayBooks()
        {
            if (!books.Any())
            {
                console.WriteLine("No books available in the library.");
                return;
            }

            console.WriteLine("\n=== Library Books ===");
            foreach (var book in books)
            {
                console.WriteLine(book.ToString());
            }
        }

        public Book? GetBook(string title, int publicationYear)
        {
            return books.FirstOrDefault(b =>
                string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase) &&
                b.PublicationYear == publicationYear);
        }

        public IEnumerable<Book> GetAllBooks() => books.AsReadOnly();
    }
}
