using Library_System.Interfaces;
using Library_System.Models;
using Library_System.Models.Members;
using Library_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Commands
{
    public class AddBookCommand : BaseCommand

    {
        private readonly IBookService bookService;

        public AddBookCommand(IConsoleService consoleService) : base(consoleService)
        {
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));

        }
        public override bool CanExecute(Member member) => member.CanAddRemoveBooks();

        public override void Execute(Member member)
        {
            if (!CanExecute(member))
            {
                consoleService.WriteLine("Access denied. You don't have permission to add books.");
                return;
            }

            string? title = GetNonEmptyInput("Title: ");
            if (title == null) return;

            string? author = GetNonEmptyInput("Author: ");
            if (author == null) return;

            int? pubYear = GetIntegerInput("Publication Year: ");
            if (pubYear == null) return;

            consoleService.WriteLine("Category (0 - Fiction, 1 - History, 2 - Child): ");
            int? categoryInt = GetIntegerInput("");
            if (categoryInt == null || categoryInt < 0 || categoryInt > 2)
            {
                consoleService.WriteLine("Invalid category.");
                return;
            }

            Book.BookCategory category = (Book.BookCategory)categoryInt.Value;
            Book book = new Book(title, author, pubYear.Value, category);
            bookService.AddBook(book);
            consoleService.WriteLine("Book added successfully.");
        }
    }
}
}
