using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata05_Delegates_Lamda
{
    public class Member : IMember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MemberLevel Level { get; set; }
        public DateTime Since { get; set; }
        public string Hotel { get; set; } = "No Hotel";

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
            public static IMember CreateRandom()
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

                var member = new Member { FirstName = FirstName, LastName = LastName, Level = Level, Since = Since };
                return member;
            }
        }
        #endregion

        public Member() { }

        #region Copy Constructor
        public Member(IMember src)
        {
            FirstName = src.FirstName;
            LastName = src.LastName;
            Level = src.Level;
            Since = src.Since;
        }
        #endregion
    }
}
