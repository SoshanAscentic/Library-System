using Library_System;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    internal class Library : ILibrary
    {
        private List<Book> books = new List<Book>();
        private List<Member> members = new List<Member>();
        private int nextMemberId = 1;

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }
            books.Add(book);
        }

        public Member AddMember(string name, int memberType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            Member newMember = memberType switch
            {
                0 => new RegularMember(name, nextMemberId++),
                1 => new MinorStaff(name, nextMemberId++),
                2 => new ManagementStaff(name, nextMemberId++),
                _ => throw new ArgumentException("Invalid member type.")
            };

            members.Add(newMember);
            return newMember;
        }

        public Member? GetMemberById(int memberId)
        {
            return members.FirstOrDefault(m => m.MemberID == memberId);
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

        public void BorrowBook(string title, int publicationYear, int memberId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }

            var member = members.FirstOrDefault(m => m.MemberID == memberId);
            if (member == null)
            {
                throw new ArgumentException("Member not found.");
            }

            if (!member.CanBorrowBooks())
            {
                throw new InvalidOperationException($"{member.GetMemberType()} cannot borrow books.");
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

        public void ReturnBook(string title, int publicationYear, int memberId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }

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

        public void DisplayMembers()
        {
            if (members.Count == 0)
            {
                Console.WriteLine("No members registered in the library.");
                return;
            }
            foreach (var member in members)
            {
                Console.WriteLine($"Name: {member.Name}, ID: {member.MemberID}, Type: {member.GetMemberType}, Borrowed Books: {member.BorrowedBooksCount}");
            }
        }
    }
}