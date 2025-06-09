using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System.Models.Staff
{
    public class ManagementStaff : Staff
    {
        public ManagementStaff(string name, int memberId) : base(name, memberId) { }
        public override string GetMemberType() => "Management Staff";
        public override bool CanViewBooks() => true;
        public override bool CanViewMembers() => true;
        public override bool CanAddRemoveBooks() => true; 
    }
    
}
