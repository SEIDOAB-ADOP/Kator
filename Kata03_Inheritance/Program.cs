using Kata03_Inheritance;

//Notice all variables are of type IMember not IRadissonMember etc, But Benefits
//is written out correctly due to polyforfism
Console.WriteLine("Create Radisson and Hilton Members");
IMember myRadisson = RadissonMember.Factory.CreateRandom();
Console.WriteLine(myRadisson);

IMember myHilton = HiltonMember.Factory.CreateRandom();
Console.WriteLine(myHilton);

//Notice that below Benefits will be "Nothing"
Console.WriteLine("\nCreate a generic Member");
IMember genericMember = Member.Factory.CreateRandom();
Console.WriteLine(genericMember);

//Notice that benefits will be correct due to Polymorfism
Console.WriteLine("\nCreate a MemberList of Radisson and Hilton members");
IMemberList memberList = MemberList.Factory.CreateRandom(20);
Console.WriteLine(memberList);

