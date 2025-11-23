using Kata06_Events;

Console.WriteLine("Create a couple of members");
var member1 = Member.Factory.CreateRandom();
Console.WriteLine($"member1: {member1}");
var member2 = Member.Factory.CreateRandom();
Console.WriteLine($"member2: {member2}");

Console.WriteLine("\nCreate a 20 Hilton members");
var HiltonMembers = MemberList.Factory.CreateRandom(20, HelloHilton);

HiltonMembers.ListSortedEvent += ListSortedEventHandler;
HiltonMembers.Sort();
Console.WriteLine(HiltonMembers);

Console.WriteLine("\nCreate a 20 Radisson members");
var RadissonMembers = MemberList.Factory.CreateRandom(20, HelloRadisson);

RadissonMembers.ListSortedEvent += ListSortedEventHandler;
RadissonMembers.Sort();
Console.WriteLine(RadissonMembers);

Console.WriteLine($"\nHilton member[0]: {HiltonMembers[0]}");
Console.WriteLine($"Radisson member[0]: {RadissonMembers[0]}");
Console.WriteLine();

Console.WriteLine("\nCreate a 10 Scandic members");
int nrBlue = 0;
var ScandicMembers = MemberList.Factory.CreateRandom(10, m =>
{
    if (m.Level == MemberLevel.Blue)
        nrBlue++;
});
Console.WriteLine(ScandicMembers);
Console.WriteLine($"Nr of Blue members: {nrBlue}");


Console.WriteLine($"HiltonMembers Gold Members: {HiltonMembers.NrOfMembers(IsGold)}");
Console.WriteLine($"RadissonMembers Gold Members: {RadissonMembers.NrOfMembers(IsGold)}");

Console.WriteLine($"HiltonMembers Gold Members: {HiltonMembers.NrOfMembers(m => m.Level == MemberLevel.Gold)}");
Console.WriteLine($"RadissonMembers Gold Members: {RadissonMembers.NrOfMembers(m => m.Level == MemberLevel.Gold)}");


#region Delegate Methods
static void HelloHilton(IMember member)
{
    Console.WriteLine($"Warm Hilton welcome {member.FirstName}!!");
    if (member.Level == MemberLevel.Platinum)
    {
        Console.WriteLine("Wow!");
    }
}
static void HelloRadisson(IMember member)
{
    Console.WriteLine($"Warm Radisson welcome {member.FirstName}!!");
}

static bool IsGold(IMember member) => member.Level == MemberLevel.Gold;
#endregion


#region #event handler
static void ListSortedEventHandler(object sender, int NrOfItems)
{
    Console.WriteLine($"#### {NrOfItems} sorted");
}
#endregion

