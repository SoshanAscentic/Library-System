using Library_System.Interfaces;
using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Services
{
    public class MenuService : IMenuService
    {
        private readonly IConsoleService consoleService;

        public MenuService(IConsoleService consoleService)
        {
            this.consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        }

        public string? DisplayAuthMenu()
        {
            consoleService.WriteLine("Welcome to the Library System!");
            consoleService.WriteLine("1. Sign up as a new member");
            consoleService.WriteLine("2. Login with Member ID");
            consoleService.Write("Please choose an option (1 or 2): ");
            return consoleService.ReadLine();
        }
        public string? DisplayMainMenu()
        {
            consoleService.WriteLine("\n--- Library Dashboard ---");
            consoleService.WriteLine("1. Add Book");
            consoleService.WriteLine("2. Remove Book");
            consoleService.WriteLine("3. Borrow Book");
            consoleService.WriteLine("4. Return Book");
            consoleService.WriteLine("5. Display Books");
            consoleService.WriteLine("6. Display Members");
            consoleService.WriteLine("0. Exit");
            consoleService.Write("Choose an option: ");
            return consoleService.ReadLine();
        }

        public void DisplayWelcome(Member member)
        {
            consoleService.Clear();
            consoleService.WriteLine($"Welcome, {member.Name}! Type: {member.GetMemberType()}");
        }
    }
}
