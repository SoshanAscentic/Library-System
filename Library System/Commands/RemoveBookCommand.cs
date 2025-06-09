using Library_System.Interfaces;
using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Commands
{
    public class RemoveBookCommand : BaseCommand
    {
        private readonly IBookService bookService;

        public RemoveBookCommand(IBookService bookService, IConsoleService consoleService): base(consoleService)
        {
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        public override bool CanExecute(Member member) => member.CanAddRemoveBooks();

        public override void Execute(Member member)
        {
            if (!CanExecute(member))
            {
                consoleService.WriteLine("Access denied. You don't have permission to remove books.");
                return;
            }

            string? title = GetNonEmptyInput("Enter title of the book to remove: ");
            if (title == null) return;

            int? year = GetIntegerInput("Enter publication year: ");
            if (year == null) return;

            bookService.RemoveBook(title, year.Value);
            consoleService.WriteLine("Book removed successfully.");
        }
    }
}
