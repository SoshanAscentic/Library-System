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

        public BookService(List<Book> books)
        {
            this.books = books ?? throw new ArgumentNullException(nameof(books));
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }
            books.Add(book);
        }

        public void RemoveBook(string title, int publicationYear)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }

            var bookToRemove = books.FirstOrDefault(b => b.Title == title && b.PublicationYear == publicationYear);
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
            }
            else
            {
                throw new ArgumentException("Book not found in the library.");
            }
        }

        public void DisplayBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books available in the library.");
                return;
            }
            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.PublicationYear}, Category: {book.Category}, Available: {book.IsAvailable}");
            }
        }

        public Book? GetBook(string title, int publicationYear)
        {
            return books.FirstOrDefault(b => b.Title == title && b.PublicationYear == publicationYear);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return books.AsReadOnly();
        }
    }
}
