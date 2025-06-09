using Library_System.Interfaces;
using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Commands
{
    public class ReturnBookCommand : BaseCommand
    {
        private readonly IBorrowingService borrowingService;

        public ReturnBookCommand(IBorrowingService borrowingService, IConsoleService consoleService)
            : base(consoleService)
        {
            this.borrowingService = borrowingService ?? throw new ArgumentNullException(nameof(borrowingService));
        }

        public override bool CanExecute(Member member) => true; //this line states that this command can be executed by any member (self note)

        public override void Execute(Member member)
        {
            string? title = GetNonEmptyInput("Enter title of the book to return: ");
            if (title == null) return;

            int? year = GetIntegerInput("Enter publication year: ");
            if (year == null) return;

            borrowingService.ReturnBook(title, year.Value, member.MemberID);
            consoleService.WriteLine("Book returned successfully.");
        }
    }
}
