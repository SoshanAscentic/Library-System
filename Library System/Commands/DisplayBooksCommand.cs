using Library_System.Interfaces;
using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Commands
{
    public class DisplayBooksCommand : BaseCommand
    {
        private readonly IBookService bookService;

        public DisplayBooksCommand(IBookService bookService, IConsoleService consoleService)
            : base(consoleService)
        {
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        public override bool CanExecute(Member member) => member.CanViewBooks();

        public override void Execute(Member member)
        {
            if (!CanExecute(member))
            {
                consoleService.WriteLine("Access denied. You cannot view books.");
                return;
            }

            bookService.DisplayBooks();
        }
    }
}
