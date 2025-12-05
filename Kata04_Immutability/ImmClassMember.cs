using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata04_Immutability
{
    public class ImmClassMember : IMember
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public MemberLevel Level { get; init; }
        public DateTime Since { get; init; }
        public string Hotel { get; init; } = "No Hotel";
        public override string ToString() => $"{FirstName} {LastName} is a {Hotel} {Level} member since {Since.Year}";

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
        
        #region Class Factory for creating an instance filled with Random data
        public static class Factory
        {
            public static ImmClassMember CreateRandom()
            {
                var rnd = new Random();
                var Level = (MemberLevel)rnd.Next((int)MemberLevel.Platinum, (int)MemberLevel.Blue + 1);

                var _fnames = "Harry, Lord, Hermione, Albus, Severus, Ron, Draco, Frodo, Gandalf, Sam, Peregrin, Saruman".Split(", ");
                var _lnames = "Potter, Voldemort, Granger, Dumbledore, Snape, Malfoy, Baggins, the Gray, Gamgee, Took, the White".Split(", ");
                var FirstName = _fnames[rnd.Next(0, _fnames.Length)];
                var LastName = _lnames[rnd.Next(0, _lnames.Length)];

                //Random DateTime between 1980 and Today
                var startDate = new DateTime(1980, 1, 1);
                var endDate = DateTime.Today;
                int range = (endDate - startDate).Days;
                var randomDays = rnd.Next(range);
                var Since = startDate.Add(TimeSpan.FromDays(randomDays));

                var member = new ImmClassMember { FirstName = FirstName, LastName = LastName, Level = Level, Since = Since };
                return member;
            }
        }
        #endregion

        #region Value change methods in an immutable class
        public ImmClassMember SetFirstName(string name) => new ImmClassMember(this) {FirstName = name};
        public ImmClassMember SetLastName(string name) => new ImmClassMember(this) {LastName = name};
        public ImmClassMember SetLevel(MemberLevel level) => new ImmClassMember(this) {Level = level};
        #endregion

        public ImmClassMember() { }

        #region Copy Constructor
        public ImmClassMember(ImmClassMember src)
        {
            this.Since = src.Since; 
            this.Level = src.Level; 
            this.FirstName = src.FirstName; 
            this.LastName = src.LastName;   
            this.Hotel = src.Hotel;
        }
        #endregion
    }
}
