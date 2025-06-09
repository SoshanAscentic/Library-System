using Library_System.Interfaces;
using Library_System.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Commands
{
    public class DisplayMembersCommand : BaseCommand
    {
        private readonly IMemberService memberService;

        public DisplayMembersCommand(IMemberService memberService, IConsoleService consoleService)
            : base(consoleService)
        {
            this.memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
        }

        public override bool CanExecute(Member member) => member.CanViewMembers();

        public override void Execute(Member member)
        {
            if (!CanExecute(member))
            {
                consoleService.WriteLine("Access denied. You cannot view members.");
                return;
            }

            memberService.DisplayMembers();
        }
    }
}
