using Library_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Services
{
    public class ConsoleService : IConsoleService
    {
        public void Clear() => Console.Clear();
        public string ReadLine() => Console.ReadLine() ?? string.Empty;
        public void Write(string message) => Console.Write(message);
        public void WriteLine(string message) => Console.WriteLine(message);
    }
}
