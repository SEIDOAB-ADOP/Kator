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
     
        #region Class Factory for creating an instance filled with Random data
        public new static class Factory
        {
            public static Member CreateRandom()
            {
                var Benefits = "R:Free breakfast, R:Late checkin, R:One free drink in the bar".Split(',');
                var member = Member.Factory.CreateRandom();
                var radissonMember = new RadissonMember
                {
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Level = member.Level,
                    Since = member.Since,
                    Benefits = Benefits
                };

                return radissonMember; 
            }
        }
        #endregion
    }
}
