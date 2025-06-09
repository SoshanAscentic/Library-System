using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Interfaces
{
    public interface IConsoleService
    {
        void WriteLine(string message);
        void Write(string message);
        string ReadLine();
        void Clear();
    }
}
