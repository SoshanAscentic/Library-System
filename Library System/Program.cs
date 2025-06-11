using Library_System.Interfaces;
using Library_System.Models;
using Library_System.Models.Members;
using Library_System.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Library_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBorrowingService, BorrowingService>();
            services.AddTransient<IConsoleService, ConsoleService>();
            services.AddTransient<IInputService, InputService>();
            services.AddTransient<IMemberService,  MemberService>();

            var menuHandler = ServiceProvider.CreateMenuHandler();
            menuHandler.Run();
        }
    }
}
