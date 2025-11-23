namespace Kata04_Immutability
{
    public record ImmRecordMember (string FirstName, string LastName, MemberLevel Level, DateTime Since) : IMember
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
        #endregion

        public override string ToString() => $"{GetType().Name}: {FirstName} {LastName} is a {Level} member since {Since.Year}";


        #region Class Factory for creating an instance filled with Random data
        public static class Factory
        {
            public static ImmRecordMember CreateRandom()
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

                        var member = new ImmRecordMember (FirstName, LastName, Level, Since);

                        return member;
                    }
                    catch { }
                }
            }
        }
        #endregion
    }
}
