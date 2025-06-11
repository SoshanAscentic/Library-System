using Library_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Services
{
    public class InputService : IInputService
    {
        private readonly IConsoleService console;

        public InputService(IConsoleService console)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }

        public string? GetNonEmptyInput(string prompt)
        {
            console.Write(prompt);
            string input = console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                console.WriteLine("Input cannot be empty.");
                return null;
            }
            return input.Trim();
        }

        public int? GetIntegerInput(string prompt)
        {
            console.Write(prompt);
            string input = console.ReadLine();

            if (int.TryParse(input, out int result))
                return result;

            console.WriteLine("Please enter a valid number.");
            return null;
        }

        public int? GetIntegerInputInRange(string prompt, int min, int max)
        {
            int? value = GetIntegerInput(prompt);

            if (value.HasValue && value >= min && value <= max)
                return value;

            console.WriteLine($"Please enter a number between {min} and {max}.");
            return null;
        }
    }
}
