using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata03_Inheritance
{
    //Notice I have inheritance (Member) and interface (IRadissonMember).
    //IMember inheritance shown in IRadissonMember
    public class RadissonMember : Member, IRadissonMember
    {
        public string RadissonOnly { get; set; } = "This is Radisson only information";
        public override string[] Benefits { get; set; }
        public override string ToString() => $"{base.ToString()}\n  Benefits: {string.Join(", ", Benefits)}";
     
        #region Class Factory for creating an instance filled with Random data
        public new static class Factory
        {
            public static IRadissonMember CreateRandom()
            {
                var member = Member.Factory.CreateRandom();
                var radissonMember = new RadissonMember(member)
                {
                    Hotel = "Radisson",
                    RadissonOnly = "This is Radisson only information",
                    Benefits = "R:Free breakfast, R:Late checkin, R:One free drink in the bar".Split(',')
                };
                return radissonMember;
            }
        }
        #endregion

        #region Copy Constructor
        public RadissonMember(IMember src) : base(src) {}
        #endregion
    }
}
