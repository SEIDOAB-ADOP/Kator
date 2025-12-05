using Kata02_IEquatable_IComparable_Factory;

Console.WriteLine("Create a couple of members");
IMember member1 = Member.Factory.CreateRandom();
Console.WriteLine($"member1: {member1}");
IMember member2 = Member.Factory.CreateRandom();
Console.WriteLine($"member2: {member2}");

Console.WriteLine("Test the copy constructor");
IMember member3 = new Member((Member)member1);
Console.WriteLine($"member3: {member3}");

Console.WriteLine("\nCreate a 20 Hotel members");
IMemberList hotelMembers = new MemberList();
for (int i = 0; i < 20; i++)
{
    hotelMembers.Add(Member.Factory.CreateRandom());
}
hotelMembers.Sort();
Console.WriteLine(hotelMembers);


Console.WriteLine("\nTest indexer");
Console.WriteLine($"Hotel member[0]: {hotelMembers[0]}");
Console.WriteLine();

Console.WriteLine("\nTest counter");
Console.WriteLine($"Nr hotelmembers joined {hotelMembers[0].Since.Year}: " +
    $"{hotelMembers.Count(hotelMembers[0].Since.Year)}");

Console.WriteLine("\nTest the deep copy and IEquatable using copy constructor");
IMemberList hotelMembersCopy = new MemberList((MemberList)hotelMembers);

Console.WriteLine("Before change:");
Console.WriteLine(hotelMembers[10]);
Console.WriteLine(hotelMembersCopy[10]);
Console.WriteLine(hotelMembers[10].Equals(hotelMembersCopy[10]));

hotelMembersCopy[10].FirstName = "Changed";
hotelMembersCopy[10].LastName = "Changed";
Console.WriteLine("After change:");
Console.WriteLine(hotelMembers[10]);
Console.WriteLine(hotelMembersCopy[10]);
Console.WriteLine(hotelMembers[10].Equals(hotelMembersCopy[10]));

