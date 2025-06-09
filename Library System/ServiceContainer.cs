using Library_System.Controllers;
using Library_System.Factories;
using Library_System.Interfaces;
using Library_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    public class ServiceContainer
    {
        public static ILibraryController CreateLibraryController()
        {
            // Core services
            ILibrary library = new Library();
            IConsoleService consoleService = new ConsoleService();

            // Application services
            IAuthenticationService authenticationService = new AuthenticationService(library.MemberService, consoleService);
            IMenuService menuService = new MenuService(consoleService);
            ICommandFactory commandFactory = new CommandFactory(library, consoleService);

            // Controller
            return new LibraryController(authenticationService, menuService, commandFactory, consoleService);
        }
    }
}
