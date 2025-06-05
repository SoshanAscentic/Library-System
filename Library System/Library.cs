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
        Member member;

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

            var bookToBorrow = books.FirstOrDefault(b => b.Title == title && b.PublicationYear == publicationYear && b.IsAvailable);
            if (bookToBorrow != null)
            {
                bookToBorrow.IsAvailable = false; // Mark the book as borrowed
                member.BorrowedBooksCount++; // Increment the borrowed books count for the member

            }
            else
            {
                throw new ArgumentException("Book not available for borrowing.");
            }

        }
    }
