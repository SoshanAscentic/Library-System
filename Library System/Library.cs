using LibrarySystem;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    internal class Library
    {
        private List<Book> books = new List<Book>();
        private List<Member> members = new List<Member>();

        public void add_book(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }
            books.Add(book);
        }

        public void add_member(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");
            }
            members.Add(member);
        }
        public void remove_book(string title, int publicationYear)
        {
            var bookToRemove = books.FirstOrDefault(b => b.Title == title && b.PublicationYear == publicationYear); //this is a LINQ query that searches for the book by title and publication year
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
            }
            else
            {
                throw new ArgumentException("Book not found in the library.");
            }
        }

        public void borrow_book(string title, int publicationYear, int memberId)
        {
            var member = members.FirstOrDefault(m => m.MemberID == memberId);
            if (member == null)
            {
                throw new ArgumentException("Member not found.");
            }

            if (member.Type != Member.MemberType.Member)
            {
                throw new InvalidOperationException("Only members of type 'Member' can borrow books.");
            }

            var bookToBorrow = books.FirstOrDefault(b => b.Title == title && b.PublicationYear == publicationYear && b.IsAvailable);
            if (bookToBorrow != null)
            {
                bookToBorrow.IsAvailable = false;
                member.BorrowedBooksCount++;
            }
            else
            {
                throw new ArgumentException("Book not available for borrowing.");
            }
        }

        public void return_book(string title, int publicationYear, int memberId)
        {
            var member = members.FirstOrDefault(m => m.MemberID == memberId);
            if (member == null)
            {
                throw new ArgumentException("Member not found.");
            }
            var bookToReturn = books.FirstOrDefault(b => b.Title == title && b.PublicationYear == publicationYear && !b.IsAvailable);
            if (bookToReturn != null)
            {
                bookToReturn.IsAvailable = true;
                member.BorrowedBooksCount--;
            }
            else
            {
                throw new ArgumentException("Book not found or not borrowed by this member.");
            }
        }

        public void display_books()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books available in the library.");
                return;
            }
            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.PublicationYear}, Category: {book.Catagory}, Available: {book.IsAvailable}");
            }
        }

        public void display_members()
        {
            if (members.Count == 0)
            {
                Console.WriteLine("No members registered in the library.");
                return;
            }
            foreach (var member in members)
            {
                Console.WriteLine($"Name: {member.Name}, ID: {member.MemberID}, Type: {member.Type}, Borrowed Books: {member.BorrowedBooksCount}");
            }
        }
    }
}