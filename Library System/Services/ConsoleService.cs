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
        void IConsoleService.Clear() => Console.Clear();
        string IConsoleService.ReadLine() => Console.ReadLine();
        void IConsoleService.Write(string message) => Console.Write(message);
        void IConsoleService.WriteLine(string message) => Console.WriteLine(message);
    }
}
