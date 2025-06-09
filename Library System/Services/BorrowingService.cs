using Library_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBookService bookService;
        private readonly IMemberService memberService;
        public BorrowingService(IBookService bookService, IMemberService memberService)
        {
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            this.memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
        }

        public void BorrowBook(string title, int publicationYear, int memberId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }

            var member = memberService.GetMemberById(memberId);
            if (member == null)
            {
                throw new ArgumentException("Member not found.");
            }

            if (!member.CanBorrowBooks())
            {
                throw new InvalidOperationException($"{member.GetMemberType()} cannot borrow books.");
            }

            var bookToBorrow = bookService.GetBook(title, publicationYear);
            if (bookToBorrow != null && bookToBorrow.IsAvailable)
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

            var member = memberService.GetMemberById(memberId);
            if (member == null)
            {
                throw new ArgumentException("Member not found.");
            }

            var bookToReturn = bookService.GetBook(title, publicationYear);
            if (bookToReturn != null && !bookToReturn.IsAvailable)
            {
                bookToReturn.IsAvailable = true;
                member.BorrowedBooksCount--;
            }
            else
            {
                throw new ArgumentException("Book not found or not borrowed by this member.");
            }
        }
    }
}
