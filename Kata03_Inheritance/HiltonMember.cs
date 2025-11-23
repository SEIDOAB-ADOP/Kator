using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata03_Inheritance
{
    //Notice I have inheritance (Member) and interface (IHiltonMember).
    //IMember inheritance shown in IHiltonMember
    internal class HiltonMember :Member, IHiltonMember
    {
        public string HiltonOnly { get; set; } = "This is Hilton only information";
        public override string[] Benefits { get; set; }
 
        #region Class Factory for creating an instance filled with Random data
        public new static class Factory
        {
            public static Member CreateRandom()
            {
                var Benefits = "H:Free breakfast, H:Room upgrade, H:Free parking".Split(',');
                var member = Member.Factory.CreateRandom();
                var hiltonMember = new HiltonMember
                {
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Level = member.Level,
                    Since = member.Since,
                    Benefits = Benefits
                };

                return hiltonMember;
            }
        }
        #endregion
    }
}
