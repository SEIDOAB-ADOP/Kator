using Kata07_IEnumerable;

Console.WriteLine("Create a couple of members");
var member1 = Member.Factory.CreateRandom();
Console.WriteLine($"member1: {member1}");
var member2 = Member.Factory.CreateRandom();
Console.WriteLine($"member2: {member2}");

Console.WriteLine("\nCreate a 20 Hilton members");
var HiltonMembers = MemberList.Factory.CreateRandom(20); 
HiltonMembers.Sort();
Console.WriteLine(HiltonMembers);

Console.WriteLine("\nCreate a 20 Radisson members");
var RadissonMembers = MemberList.Factory.CreateRandom(20);
RadissonMembers.Sort();
Console.WriteLine(RadissonMembers);

Console.WriteLine($"\nHilton member[0]: {HiltonMembers[0]}");
Console.WriteLine($"Radisson member[0]: {RadissonMembers[0]}");
Console.WriteLine();

#region IEnumerable
Console.WriteLine("\nIterate over Hiltonmembers with foreach");
foreach (var member in HiltonMembers)
{
    Console.WriteLine(member);
}
#endregion