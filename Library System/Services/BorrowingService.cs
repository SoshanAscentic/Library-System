using Library_System.Interfaces;
using Library_System.Models.Members;
using Library_System.Models.Staff;
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
            var member = GetValidMember(memberId);
            if (!member.CanBorrowBooks())
                throw new InvalidOperationException($"{member.GetMemberType()} cannot borrow books.");

            var book = bookService.GetBook(title, publicationYear);
            if (book == null || !book.IsAvailable)
                throw new InvalidOperationException("Book not available for borrowing.");

            book.IsAvailable = false;
            member.BorrowedBooksCount++;
        }

        public void ReturnBook(string title, int publicationYear, int memberId)
        {
            var member = GetValidMember(memberId);
            var book = bookService.GetBook(title, publicationYear);

            if (book == null || book.IsAvailable)
                throw new InvalidOperationException("Book not found or not currently borrowed.");

            book.IsAvailable = true;
            if (member.BorrowedBooksCount > 0)
                member.BorrowedBooksCount--;
        }

        private Member GetValidMember(int memberId)
        {
            return memberService.GetMemberById(memberId) ??
                   throw new InvalidOperationException("Member not found.");
        }
    }
}
