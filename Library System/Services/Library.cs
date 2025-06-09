using Library_System.Interfaces;
using Library_System.Models;
using Library_System.Models.Members;
using Library_System.Models.Staff;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Services
{
    public class Library : ILibrary
    {
        private readonly List<Book> books;
        private readonly List<Member> members;

        public IBookService BookService { get; }
        public IMemberService MemberService { get; }
        public IBorrowingService BorrowingService { get; }

        public Library()
        {
            books = new List<Book>();
            members = new List<Member>();

            BookService = new BookService(books);
            MemberService = new MemberService(members);
            BorrowingService = new BorrowingService(BookService, MemberService);
        }
    }
}