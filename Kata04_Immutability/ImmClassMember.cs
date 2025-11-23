using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata04_Immutability
{
    public class ImmClassMember : IMember
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public MemberLevel Level { get; private set; }
        public DateTime Since { get; private set; }

        public override string ToString() => $"{GetType().Name}: {FirstName} {LastName} is a {Level} member since {Since.Year}";

        #region Implement IComparable
        public int CompareTo(IMember other)
        {
            if (Level != other.Level)
                return Level.CompareTo(other.Level);

            if (LastName != other.LastName)
                return LastName.CompareTo(other.LastName);

            if (FirstName != other.FirstName)
                return FirstName.CompareTo(other.FirstName);
            
            return Since.CompareTo(other.Since);
        }
        #endregion

        #region Implement IEquatable
        public bool Equals(IMember other) => (this.FirstName, this.LastName, this.Level, this.Since) == 
            (other.FirstName, other.LastName, other.Level, other.Since);

        // legacy .NET compliance
        public override bool Equals(object obj) => Equals(obj as IMember);
        public override int GetHashCode() => (this.FirstName, this.LastName, this.Level, this.Since).GetHashCode();
        #endregion
        
        #region operator overload
        public static bool operator ==(IMember left, ImmClassMember right) => left.Equals(right);
        public static bool operator !=(IMember left, ImmClassMember right) => !left.Equals(right);
        #endregion

        #region Class Factory for creating an instance filled with Random data
        internal static class Factory
        {
            internal static ImmClassMember CreateRandom()
            {
                var rnd = new Random();
                while (true)
                {
                    try
                    {
                        int year = rnd.Next(1980, DateTime.Today.Year + 1);
                        int month = rnd.Next(1, 13);
                        int day = rnd.Next(1, 31);

                        var Since = new DateTime(year, month, day);
                        var Level = (MemberLevel)rnd.Next((int)MemberLevel.Platinum, (int)MemberLevel.Blue + 1);

                        string[] _firstnames = "Fred John Mary Jane Oliver Marie".Split(' ');
                        string[] _lastnames = "Johnsson Pearsson Smith Ewans Andersson".Split(' ');
                        var FirstName = _firstnames[rnd.Next(0, _firstnames.Length)];
                        var LastName = _lastnames[rnd.Next(0, _lastnames.Length)];

                        var member = new ImmClassMember { FirstName = FirstName, LastName = LastName, Level = Level, Since = Since };
                        return member;
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region Value change methods in an immutable class
        public ImmClassMember SetFirstName(string name)
        {
            var newMember = new ImmClassMember(this) {FirstName = name};
            return newMember;
        }
        public ImmClassMember SetLastName(string name)
        {
            var newMember = new ImmClassMember(this);
            newMember.LastName = name;
            return newMember;
        }
        public ImmClassMember SetLevel(MemberLevel level)
        {
            var newMember = new ImmClassMember(this);
            newMember.Level = level;
            return newMember;
        }
        #endregion
        public ImmClassMember() { }

        #region Copy Constructor
        public ImmClassMember(ImmClassMember src)
        {
            this.Since = src.Since; 
            this.Level = src.Level; 
            this.FirstName = src.FirstName; 
            this.LastName = src.LastName;   
        }
        #endregion
    }
}
