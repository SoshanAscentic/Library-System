using Library_System.Interfaces;
using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Controllers
{
    public class LibraryController : ILibraryController
    {
        private readonly IAuthenticationService authenticationService;
        private readonly ICommandFactory commandFactory;
        private readonly IConsoleService consoleService;
        private readonly IMenuService menuService;

        public LibraryController(IAuthenticationService authenticationService, IMenuService menuService, ICommandFactory commandFactory, IConsoleService consoleService)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
            this.commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            this.consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        }
        public void run()
        {
            Member? loggedInMember = AuthenticateUser();
            if (loggedInMember == null) return;

            menuService.DisplayWelcome(loggedInMember);
            RunMainLoop(loggedInMember);
        }

        private Member? AuthenticateUser()
        {
            Member? loggedInMember = null;

            while (loggedInMember == null)
            {
                string? input = menuService.DisplayAuthMenu();

                loggedInMember = input switch
                {
                    "1" => authenticationService.SignUp(),
                    "2" => authenticationService.Login(),
                    _ => HandleInvalidAuthOption()
                };
            }

            return loggedInMember;
        }

        private Member? HandleInvalidAuthOption()
        {
            consoleService.WriteLine("Invalid option.");
            return null;
        }

        private void RunMainLoop(Member loggedInMember)
        {
            var commands = commandFactory.CreateCommands();

            while (true)
            {
                try
                {
                    string? option = menuService.DisplayMainMenu();

                    if (option == "0")
                    {
                        consoleService.WriteLine("Exiting...");
                        return;
                    }

                    if (commands.TryGetValue(option ?? "", out var command))
                    {
                        command.Execute(loggedInMember);
                    }
                    else
                    {
                        consoleService.WriteLine("Invalid option.");
                    }
                }
                catch (Exception ex)
                {
                    consoleService.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
