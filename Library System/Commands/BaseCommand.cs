using Library_System.Interfaces;
using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected readonly IConsoleService consoleService;

        protected BaseCommand(IConsoleService consoleService)
        {
            this.consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        }
        public abstract bool CanExecute(Member member);
        public abstract void Execute(Member member);

        protected string? GetNonEmptyInput(string prompt)
        {
            consoleService.Write(prompt);
            string? input = consoleService.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                consoleService.WriteLine("Input cannot be empty.");
                return null;
            }
            return input;
        }

        protected int? GetIntegerInput(string prompt)
        {
            consoleService.Write(prompt);
            string? input = consoleService.ReadLine();

            if (int.TryParse(input, out int result))
                return result;

            consoleService.WriteLine("Invalid input.");
            return null;
        }

    }
}
