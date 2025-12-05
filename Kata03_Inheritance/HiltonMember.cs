using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata03_Inheritance
{
    //Notice I have inheritance (Member) and interface (IHiltonMember).
    //IMember inheritance shown in IHiltonMember
    public class HiltonMember :Member, IHiltonMember
    {
        public string HiltonOnly { get; set; } = "This is Hilton only information";
        public override string[] Benefits { get; set; }
 
        public override string ToString() => $"{base.ToString()}\n  Benefits: {string.Join(", ", Benefits)}";

        #region Class Factory for creating an instance filled with Random data
        public new static class Factory
        {
            public static IHiltonMember CreateRandom()
            {
                var member = Member.Factory.CreateRandom();
                var hiltonMember = new HiltonMember(member)
                {
                    Hotel = "Hilton",
                    HiltonOnly = "This is Hilton only information",
                    Benefits = "H:Free breakfast, H:Room upgrade, H:Free parking".Split(',')
                };
                return hiltonMember;
            }
        }
        #endregion

        #region Copy Constructor
        public HiltonMember(IMember src) : base(src) {}
        #endregion
    }
}
