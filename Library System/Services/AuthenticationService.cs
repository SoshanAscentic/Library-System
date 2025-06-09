using Library_System.Interfaces;
using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IMemberService memberService;
        private readonly IConsoleService consoleService;

        public AuthenticationService(IMemberService memberService, IConsoleService consoleService)
        {
            this.memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            this.consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        }

        public Member? Signup()
        {
            consoleService.WriteLine("Enter your name: ");
            string? name = consoleService.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                consoleService.WriteLine("Name cannot be empty.");
                return null;
            }

            consoleService.WriteLine("Select member type:");
            consoleService.WriteLine("0 - Member");
            consoleService.WriteLine("1 - Minor Staff");
            consoleService.WriteLine("2 - Management Staff");

            string? typeInputStr = consoleService.ReadLine();
            if (!int.TryParse(typeInputStr, out int typeInput) || typeInput < 0 || typeInput > 2)
            {
                consoleService.WriteLine("Invalid member type.");
                return null;
            }

            Member newMember = memberService.AddMember(name, typeInput);
            consoleService.WriteLine($"Successfully signed up! Your Member ID is: {newMember.MemberID}");
            return newMember;
        }

        public Member? Login()
        {
            consoleService.WriteLine("Enter your Member ID: ");
            string? memberIdStr = consoleService.ReadLine();

            if (!int.TryParse(memberIdStr, out int enteredId))
            {
                consoleService.WriteLine("Invalid input.");
                return null;
            }

            var existingMember = memberService.GetMemberById(enteredId);
            if (existingMember != null)
            {
                consoleService.WriteLine($"Welcome back, {existingMember.Name}!");
                return existingMember;
            }
            else
            {
                consoleService.WriteLine("Member not found. Please sign up first.");
                return null;
            }
        }
    }
}
