using Library_System.Handlers;
using Library_System.Interfaces;
using Library_System.Models;
using Library_System.Models.Members;
using Library_System.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Library_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            // Register data collections as singletons (shared across all services and transient will create new instances each time)
            services.AddSingleton<List<Book>>();
            services.AddSingleton<List<Member>>();

            // Register services as transients
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBorrowingService, BorrowingService>();
            services.AddTransient<IConsoleService, ConsoleService>();
            services.AddTransient<IInputService, InputService>();
            services.AddTransient<IMemberService, MemberService>();

            // Register MenuHandler
            services.AddTransient<MenuHandler>();

            // Build the service provider and resolve MenuHandler
            var serviceProvider = services.BuildServiceProvider();
            var menuHandler = serviceProvider.GetRequiredService<MenuHandler>();

            menuHandler.Run();
        }
    }
}