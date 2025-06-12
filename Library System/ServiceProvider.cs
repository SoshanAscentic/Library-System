//using Library_System.Handlers;
//using Library_System.Interfaces;
//using Library_System.Models;
//using Library_System.Services;
//using System;
//using System.Collections.Generic;

//namespace Library_System
//{
//    public static class ServiceProvider
//    {
//        public static MenuHandler CreateMenuHandler()
//        {
//            // Data stores
//            var books = new List<Book>();
//            var members = new List<Member>();

//            // Core services
//            IConsoleService console = new ConsoleService();
//            IInputService input = new InputService(console);

//            // Business services
//            IBookService bookService = new BookService(books, console);
//            IMemberService memberService = new MemberService(members, console);
//            IBorrowingService borrowingService = new BorrowingService(bookService, memberService);

//            // Menu handler
//            return new MenuHandler(bookService, memberService, borrowingService, console, input);
//        }
//    }
//}