using Kata04_Immutability;

Console.WriteLine("Create a couple of immutable members");

var member1 = ImmClassMember.Factory.CreateRandom();
Console.WriteLine($"\nmember1 of type {member1.GetType().Name}: {member1}");
var newMember1 = member1.SetFirstName("Karl").SetLastName("Petterson").SetLevel(MemberLevel.Platinum);
Console.WriteLine($"Modified Member from immutable member1: {newMember1}");

var member2 = ImmRecordMember.Factory.CreateRandom();
Console.WriteLine($"\nmember2 of type {member2.GetType().Name}: {member2}");
var newMember2 = member2 with { FirstName = "Karl", LastName = "Petterson" };
Console.WriteLine($"Modified Member from immutable member2: {newMember2}");

Console.WriteLine("\nCreate a 20 Hilton members of mixed types");
var HiltonMembers = MemberList.Factory.CreateRandom(20); 
HiltonMembers.Sort();
Console.WriteLine(HiltonMembers);

Console.WriteLine("\nCreate a 20 Radisson members of mixed types");
var RadissonMembers = MemberList.Factory.CreateRandom(20);
RadissonMembers.Sort();
Console.WriteLine(RadissonMembers);

Console.WriteLine($"\nHilton member[0]: {HiltonMembers[0]}");
Console.WriteLine($"Radisson member[0]: {RadissonMembers[0]}");
Console.WriteLine();
