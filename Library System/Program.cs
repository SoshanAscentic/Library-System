using Library_System.Controllers;
using Library_System.Factories;
using Library_System.Interfaces;
using Library_System.Models;
using Library_System.Models.Members;
using Library_System.Services;
using System;

namespace Library_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var controller = ServiceContainer.CreateLibraryController();
            controller.run();
        }
    }
}
