using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata04_Immutability
{
    public enum MemberLevel { Platinum, Gold, Silver, Blue}
    public interface IMember: IEquatable<IMember>, IComparable<IMember>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public MemberLevel Level {get; init; }
        public DateTime Since { get; init; }
        public string Hotel { get; init; }
    }
}
