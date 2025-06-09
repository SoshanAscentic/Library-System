using Library_System.Models;
using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Interfaces
{

    public interface ILibrary
    {
        IBookService BookService { get; }
        IMemberService MemberService { get; }
        IBorrowingService BorrowingService { get; } 
    }
}
