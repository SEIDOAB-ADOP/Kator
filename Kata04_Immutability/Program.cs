using Kata04_Immutability;

Console.WriteLine("Create a couple of immutable members");

var member1 = ImmClassMember.Factory.CreateRandom();
Console.WriteLine($"\nmember1 of type {member1.GetType().Name}: {member1}");

var newMember1 = ((ImmClassMember)member1).SetFirstName("Karl").SetLastName("Petterson").SetLevel(MemberLevel.Platinum);
Console.WriteLine($"Modified Member from immutable member1: {newMember1}");

var member2 = ImmRecordMember.Factory.CreateRandom();
Console.WriteLine($"\nmember2 of type {member2.GetType().Name}: {member2}");

var newMember2 = (ImmRecordMember) member2 with { FirstName = "Karl", LastName = "Petterson" };
Console.WriteLine($"Modified Member from immutable member2: {newMember2}");



Console.WriteLine("\nCreate a 20 Hotel members");
IMemberList hotelMembers = new MemberList();
for (int i = 0; i < 5; i++)
{
    hotelMembers.Add(ImmClassMember.Factory.CreateRandom());
    hotelMembers.Add(ImmRecordMember.Factory.CreateRandom());
}
Console.WriteLine(hotelMembers);

Console.WriteLine("\nTest the deep copy and == operator");
IMemberList hotelMembersCopy = new MemberList((MemberList)hotelMembers);

Console.WriteLine(hotelMembers[0].Equals(hotelMembersCopy[0]));
Console.WriteLine(hotelMembers[1].Equals(hotelMembersCopy[1]));