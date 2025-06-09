using Library_System.Commands;
using Library_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private readonly ILibrary library;
        private readonly IConsoleService consoleService;

        public CommandFactory(ILibrary library, IConsoleService consoleService)
        {
            this.library = library ?? throw new ArgumentNullException(nameof(library));
            this.consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        }

        public Dictionary<string, ICommand> CreateCommands()
        {
            return new Dictionary<string, ICommand>
            {
                ["1"] = new AddBookCommand(library.BookService, consoleService),
                ["2"] = new RemoveBookCommand(library.BookService, consoleService),
                ["3"] = new BorrowBookCommand(library.BorrowingService, consoleService),
                ["4"] = new ReturnBookCommand(library.BorrowingService, consoleService),
                ["5"] = new DisplayBooksCommand(library.BookService, consoleService),
                ["6"] = new DisplayMembersCommand(library.MemberService, consoleService)
            };
        }
    }

}
