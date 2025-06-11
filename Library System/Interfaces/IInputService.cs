using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Interfaces
{
    public interface IInputService
    {
        string? GetNonEmptyInput(string prompt);
        int? GetIntegerInput(string prompt);
        int? GetIntegerInputInRange(string prompt, int min, int max);
    }
}
