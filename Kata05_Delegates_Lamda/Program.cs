using Kata05_Delegates_Lamda;

Console.WriteLine("\nCreate a 20 Hilton members");
var HiltonMembers = MemberList.Factory.CreateRandom(20, HelloHilton); 
HiltonMembers.Sort();
Console.WriteLine(HiltonMembers);

Console.WriteLine("\nCreate a 20 Radisson members");
var RadissonMembers = MemberList.Factory.CreateRandom(20, HelloRadisson);
RadissonMembers.Sort();
Console.WriteLine(RadissonMembers);

Console.WriteLine($"\nHilton member[0]: {HiltonMembers[0]}");
Console.WriteLine($"Radisson member[0]: {RadissonMembers[0]}");
Console.WriteLine();

Console.WriteLine("\nCreate a 10 Scandic members");
int nrBlueScandic = 0;
var ScandicMembers = MemberList.Factory.CreateRandom(10, m =>
{
    m.Hotel = "Scandic";
    if (m.Level == MemberLevel.Blue)
        nrBlueScandic++;
    return m;
});

Console.WriteLine(ScandicMembers);
Console.WriteLine($"Nr of Blue Scandic members: {nrBlueScandic}");

Console.WriteLine($"HiltonMembers Gold Members: {HiltonMembers.Filter(IsGold)}");
Console.WriteLine($"RadissonMembers Gold Members: {RadissonMembers.Filter(m => m.Level == MemberLevel.Gold)}");


#region Delegate Methods
static IMember HelloHilton(IMember member)
{
    member.Hotel = "Hilton";

    Console.WriteLine($"Warm Hilton welcome {member.FirstName} {member.LastName}!!");
    if (member.Level == MemberLevel.Platinum)
    {
        Console.WriteLine("Wow!");
    }
    return member;
}

static IMember HelloRadisson(IMember member)
{
    member.Hotel = "Radisson";
    Console.WriteLine($"Warm Radisson welcome {member.FirstName} {member.LastName}!!");
    return member;
}

static bool IsGold(IMember member) => member.Level == MemberLevel.Gold;
#endregion

