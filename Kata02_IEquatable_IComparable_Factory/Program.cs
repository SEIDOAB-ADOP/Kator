using Kata02_IEquatable_IComparable_Factory;

Console.WriteLine("Create a couple of members");
IMember member1 = Member.Factory.CreateRandom();
Console.WriteLine($"member1: {member1}");
IMember member2 = Member.Factory.CreateRandom();
Console.WriteLine($"member2: {member2}");

Console.WriteLine("Test the copy constructor");
IMember member3 = new Member((Member)member1);
Console.WriteLine($"member3: {member3}");

Console.WriteLine("\nCreate a 20 Hilton members");
IMemberList HiltonMembers = MemberList.Factory.CreateRandom(20);
HiltonMembers.Sort();
Console.WriteLine(HiltonMembers);

Console.WriteLine("\nCreate a 20 Radisson members");
IMemberList RadissonMembers = MemberList.Factory.CreateRandom(20);
RadissonMembers.Sort();
Console.WriteLine(RadissonMembers);

Console.WriteLine("\nTest indexer");
Console.WriteLine($"Hilton member[0]: {HiltonMembers[0]}");
Console.WriteLine($"Radisson member[0]: {RadissonMembers[0]}");
Console.WriteLine();

Console.WriteLine("\nTest counter");
Console.WriteLine($"Nr HiltonMembers joined {HiltonMembers[0].Since.Year}: " +
    $"{HiltonMembers.Count(HiltonMembers[0].Since.Year)}");
Console.WriteLine($"Nr RadissonMembers joined {RadissonMembers[0].Since.Year}: " +
    $"{HiltonMembers.Count(RadissonMembers[0].Since.Year)}");

Console.WriteLine("\nTest the deep copy using copy constructor");

//I make a cast IMemberList -> MemberList of HiltonMembers to be able to use
//the copy contructor as the list _member is private
IMemberList copyList = new MemberList((MemberList)HiltonMembers);

Console.WriteLine("Before change:");
Console.WriteLine(HiltonMembers[10]);
Console.WriteLine(copyList[10]);

HiltonMembers[10].FirstName = "Test";
Console.WriteLine("After change:");
Console.WriteLine(HiltonMembers[10]);
Console.WriteLine(copyList[10]);


