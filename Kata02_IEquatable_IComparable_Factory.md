# Kata02: IEquatable, IComparable & Factory

## Overview
This kata demonstrates encapsulation of a hotel loyalty program member list using classes, interfaces, `IEquatable<T>`, `IComparable<T>`, and the Factory pattern for creating instances with random data.

## Project Structure

### 1. Create Project
Create a new C# console project named `Kata02_IEquatable_IComparable_Factory`.

### 2. IMember Interface
Define an interface `IMember` with the following members:

```csharp
public enum MemberLevel { Platinum, Gold, Silver, Blue }

public interface IMember : IEquatable<IMember>, IComparable<IMember>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public MemberLevel Level { get; set; }
    public DateTime Since { get; set; }
}
```

**Key points:**
- Declare `MemberLevel` enum in the same file as `IMember` (they belong together)
- Interface inherits from `IEquatable<IMember>` and `IComparable<IMember>`

### 3. Member Class Implementation
Implement the `Member` class with:

#### IComparable Implementation
Sort members by:
1. Bonus level (MemberLevel)
2. Last name
3. First name
4. Membership start date

```csharp
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
```

#### IEquatable Implementation
```csharp
public bool Equals(IMember other) => 
    (this.FirstName, this.LastName, this.Level, this.Since) == 
    (other.FirstName, other.LastName, other.Level, other.Since);

// Legacy .NET compliance
public override bool Equals(object obj) => Equals(obj as IMember);
public override int GetHashCode() => 
    (this.FirstName, this.LastName, this.Level, this.Since).GetHashCode();
```

#### Constructors
- Default constructor: `public Member() { }`
- Copy constructor: `public Member(IMember src) { ... }`

#### ToString Override
```csharp
public override string ToString() => 
    $"{FirstName} {LastName} is a {Level} member since {Since.Year}";
```

### 4. Factory Pattern - Member.Factory
Create random Member instances using a nested static class:

```csharp
public static class Factory
{
    public static Member CreateRandom()
    {
        var rnd = new Random();
        var Level = (MemberLevel)rnd.Next((int)MemberLevel.Platinum, 
                                          (int)MemberLevel.Blue + 1);
        
        var _fnames = "Harry, Lord, Hermione, Albus, Severus, Ron, Draco, Frodo, Gandalf, Sam, Peregrin, Saruman".Split(", ");
        var _lnames = "Potter, Voldemort, Granger, Dumbledore, Snape, Malfoy, Baggins, the Gray, Gamgee, Took, the White".Split(", ");
        var FirstName = _fnames[rnd.Next(0, _fnames.Length)];
        var LastName = _lnames[rnd.Next(0, _lnames.Length)];
        
        // Random DateTime between 1980 and Today using TimeSpan
        var startDate = new DateTime(1980, 1, 1);
        var endDate = DateTime.Today;
        int range = (endDate - startDate).Days;
        var randomDays = rnd.Next(range);
        var Since = startDate.Add(TimeSpan.FromDays(randomDays));
        
        return new Member { FirstName = FirstName, LastName = LastName, 
                           Level = Level, Since = Since };
    }
}
```

**Note:** Random date generation uses `TimeSpan.FromDays()` for cleaner implementation without exception handling.

### 5. Testing Member Creation
In `Program.Main()`, test creating Member instances:

```csharp
IMember member1 = Member.Factory.CreateRandom();
IMember member2 = Member.Factory.CreateRandom();
IMember member3 = new Member((Member)member1); // Copy constructor
```

### 6. IMemberList Interface
Define an interface for managing member collections:

```csharp
public interface IMemberList
{
    public IMember this[int idx] { get; }  // Indexer
    int Count();
    int Count(int year);
    void Sort();
}
```

### 7. MemberList Class Implementation
Implement `MemberList` with:

```csharp
class MemberList : IMemberList
{
    private List<IMember> _members = new List<IMember>();
    
    public IMember this[int idx] => _members[idx];
    
    public int Count() => _members.Count;
    
    public int Count(int year) => _members.Count(item => item.Since.Year == year);
    
    public void Sort() => _members.Sort();
    
    public override string ToString()
    {
        string sRet = "";
        for (int i = 0; i < _members.Count; i++)
        {
            sRet += $"{_members[i]}\n";
            if ((i + 1) % 10 == 0)
                sRet += "\n";
        }
        return sRet;
    }
}
```

**Key implementation notes:**
- Use `IMember` type everywhere except when creating new `Member` instances
- Indexer provides read-only access to members
- `ToString()` outputs members in clusters of 10

### 8. Factory Pattern - MemberList.Factory
Create a factory for generating member lists with random data:

```csharp
public static class Factory
{
    public static MemberList CreateRandom(int NrOfItems)
    {
        var memberlist = new MemberList();
        for (int i = 0; i < NrOfItems; i++)
        {
            memberlist._members.Add(Member.Factory.CreateRandom());
        }
        return memberlist;
    }
}
```

### 9. Copy Constructor - MemberList
The copy constructor demonstrates three types of copies:

```csharp
public MemberList(MemberList org)
{
    // Reference Copy (not recommended)
    _members = org._members;
    
    // Shallow Copy
    _members = new List<IMember>(org._members);
    
    // Deep Copy using LINQ
    _members = org._members.Select(o => new Member(o)).ToList<IMember>();
}
```

### 10. Program.Main() - Complete Example

```csharp
// Create and sort Hilton members
IMemberList HiltonMembers = MemberList.Factory.CreateRandom(20);
HiltonMembers.Sort();
Console.WriteLine(HiltonMembers);

// Create and sort Radisson members
IMemberList RadissonMembers = MemberList.Factory.CreateRandom(20);
RadissonMembers.Sort();
Console.WriteLine(RadissonMembers);

// Test indexer
Console.WriteLine($"Hilton member[0]: {HiltonMembers[0]}");
Console.WriteLine($"Radisson member[0]: {RadissonMembers[0]}");

// Test counter
Console.WriteLine($"Nr HiltonMembers joined {HiltonMembers[0].Since.Year}: " +
    $"{HiltonMembers.Count(HiltonMembers[0].Since.Year)}");

// Test deep copy
IMemberList copyList = new MemberList((MemberList)HiltonMembers);
```

## Key Learning Points

1. **Interface Design**: Define contracts with `IEquatable<T>` and `IComparable<T>` for standard .NET list operations
2. **Sorting**: Implement `CompareTo()` for multi-level sorting logic
3. **Equality**: Use tuple comparison for clean `Equals()` implementation
4. **Factory Pattern**: Create nested static Factory classes for object initialization
5. **Random Data**: Use `TimeSpan` for clean random date generation
6. **Copy Strategies**: Understand reference, shallow, and deep copying
7. **Type Usage**: Prefer interface types (`IMember`, `IMemberList`) in variable declarations

## Practice Goals

This kata should be practiced until the concepts become part of long-term memory and muscle memory. Focus on:
- Writing interfaces with generic constraints
- Implementing comparison and equality properly
- Using the Factory pattern for test data generation
- Understanding different copy mechanisms

**Good luck! Keep practicing!**
