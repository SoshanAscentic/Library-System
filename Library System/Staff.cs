using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    public abstract class Staff : Member
    {
        public Staff(string name, int memberId) : base(name, memberId)
        {
        }

        public override bool CanAddRemoveBooks() => true;

    }
    
    
}
