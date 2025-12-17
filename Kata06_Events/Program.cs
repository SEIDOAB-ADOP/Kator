using Kata06_Events;

Console.WriteLine("\nCreate a 20 Hilton members");
var HiltonMembers = MemberList.Factory.CreateRandom(20, HelloHilton); 
HiltonMembers.Sort();
Console.WriteLine(HiltonMembers);

Console.WriteLine("\nCreate a 20 Radisson members");
var RadissonMembers = MemberList.Factory.CreateRandom(20, HelloRadisson);
RadissonMembers.Sort();
Console.WriteLine(RadissonMembers);

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


//Subscribe to events
HiltonMembers.PlatinumMemberEvent += PlatinumMemberEventHandler;
RadissonMembers.PlatinumMemberEvent += PlatinumMemberEventHandler;
RadissonMembers.GoldMemberEvent += GoldMemberEventHandler;

//Create new members to see events in action
Console.WriteLine("\nAdding new Hotel Members - Catching Gold and Platinum events:");
for (int i = 0; i < 20; i++)
{
    HiltonMembers.Add(Member.Factory.CreateRandom());
    RadissonMembers.Add(Member.Factory.CreateRandom());
}



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

#region #event handler
static void PlatinumMemberEventHandler(object sender, IMember member)
{
    Console.WriteLine($"#### {member.FirstName} {member.LastName} is a Platinum member! ####");
}
static void GoldMemberEventHandler(object sender, IMember member)
{
    Console.WriteLine($"#### {member.FirstName} {member.LastName} is a Gold member! ####");
}
#endregion