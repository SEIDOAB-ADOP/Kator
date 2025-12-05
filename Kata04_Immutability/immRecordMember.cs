namespace Kata04_Immutability
{
    public record ImmRecordMember (string FirstName, string LastName, MemberLevel Level, DateTime Since, string Hotel) : IMember
    {

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

        //only needed because of IEquatable<IMember>
        public bool Equals(IMember other) => this == (ImmRecordMember)other;
        #endregion

        public override string ToString() => $"{FirstName} {LastName} is a {Level} member since {Since.Year}";


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

                var member = new ImmRecordMember (FirstName, LastName, Level, Since, "No Hotel");
                return member;
            }
        }
        #endregion
    }
}
