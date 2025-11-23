using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata04_Immutability
{
    public enum MemberLevel { Platinum, Gold, Silver, Blue}
    public interface IMember: IComparable<IMember>
    {
        public string FirstName { get; }
        public string LastName { get; }
        public MemberLevel Level {get; }
        public DateTime Since { get; }
    } 
}
