using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Interfaces
{
    public interface IBorrowingService
    {
        void BorrowBook(string title, int publicationYear, int memberId);
        void ReturnBook(string title, int publicationYear, int memberId
    }
}
