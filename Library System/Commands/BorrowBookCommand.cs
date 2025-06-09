using Library_System.Interfaces;
using Library_System.Models.Members;
using Library_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Commands
{
    public class BorrowBookCommand : BaseCommand
    {
        private readonly IBorrowingService borrowingService;

        public BorrowBookCommand(IBorrowingService borrowingService, IConsoleService consoleService)
            : base(consoleService)
        {
            this.borrowingService = borrowingService ?? throw new ArgumentNullException(nameof(borrowingService));
        }

        public override bool CanExecute(Member member) => member.CanBorrowBooks();

        public override void Execute(Member member)
        {
            if (!CanExecute(member))
            {
                consoleService.WriteLine("Access denied. You cannot borrow books.");
                return;
            }

            string? title = GetNonEmptyInput("Enter title of the book to borrow: ");
            if (title == null) return;

            int? year = GetIntegerInput("Enter publication year: ");
            if (year == null) return;

            borrowingService.BorrowBook(title, year.Value, member.MemberID);
            consoleService.WriteLine("Book borrowed successfully.");
        }
    }
}
