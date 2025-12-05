# Kata03: Inheritance & Polymorphism

## Overview
This kata builds on Kata02 and focuses on practicing inheritance and polymorphism in C#.

## Project Setup

### 1. Create New Project
Create a new project named `Kata03_Inheritance` and copy all code files from Kata02 into the new project.

### 2. Update Namespace
Change the namespace in all files to `Kata03_Inheritance`.

## Interface Development

### 3. Add Benefits Property to IMember
Add the following property to the `IMember` interface:

```csharp
public string[] Benefits { get; set; }
```

### 4. Create IRadissonMember Interface
In file `IRadissonMember.cs`, create a new interface that inherits from `IMember`:

```csharp
public interface IRadissonMember : IMember
{
    public string RadissonOnly { get; set; }
}
```

**Key point:** Interface inheritance is shown in the declaration.

### 5. Create IHiltonMember Interface
In file `IHiltonMember.cs`, create a new interface that inherits from `IMember`:

```csharp
public interface IHiltonMember : IMember
{
    public string HiltonOnly { get; set; }
}
```

### 6. Disable Nullable Reference Types
Double-click the project file and disable nullable reference types:

```xml
<Nullable>disable</Nullable>
```

## Class Implementation

### 7. Update Member Class
In the `Member` class:

1. **Implement Benefits property as virtual:**
```csharp
public virtual string[] Benefits { get; set; } = { "Nothing", "Nothing" };
```

2. **Add Hotel to ToString():**
```csharp
public override string ToString() => 
    $"{FirstName} {LastName} is a {Hotel} {Level} member since {Since.Year}";
```

3. **Update Copy Constructor to include Benefits:**
```csharp
public Member(IMember src)
{
    FirstName = src.FirstName;
    LastName = src.LastName;
    Level = src.Level;
    Since = src.Since;
    Benefits = src.Benefits;
}
```

**Key point:** The `virtual` keyword makes Benefits polymorphic.

### 8. Create RadissonMember Class
Create `RadissonMember` class in its own file that inherits from `Member` and implements `IRadissonMember`:

```csharp
public class RadissonMember : Member, IRadissonMember
{
    public string RadissonOnly { get; set; } = "This is Radisson only information";
    public override string[] Benefits { get; set; }
    
    public override string ToString() => 
        $"{base.ToString()}\n  Benefits: {string.Join(", ", Benefits)}";
}
```

**Key points:**
- Class shows both inheritance (`Member`) and interface implementation (`IRadissonMember`)
- `IMember` inheritance is shown in `IRadissonMember`
- `override` on Benefits property enables polymorphism (works with `virtual` in Member)

### 9. Add Factory to RadissonMember
Declare a factory class in `RadissonMember`:

```csharp
public new static class Factory
{
    public static IRadissonMember CreateRandom()
    {
        var member = Member.Factory.CreateRandom();
        var radissonMember = new RadissonMember(member)
        {
            Hotel = "Radisson",
            RadissonOnly = "This is Radisson only information",
            Benefits = "R:Free breakfast, R:Late checkin, R:One free drink in the bar".Split(',')
        };
        return radissonMember;
    }
}
```

**Implementation steps:**
1. Create a Member instance using `Member.Factory.CreateRandom()`
2. Create RadissonMember using copy constructor and object initialization
3. Set Hotel, RadissonOnly, and Benefits with Radisson-specific data
4. Return the RadissonMember instance

**Note:** Use `new` keyword to hide the inherited Factory class.

### 10. Add Copy Constructor to RadissonMember
```csharp
public RadissonMember(IMember src) : base(src) {}
```

### 11. Test RadissonMember
Modify `Main()` to create an instance of RadissonMember:

```csharp
IMember myRadisson = RadissonMember.Factory.CreateRandom();
Console.WriteLine(myRadisson);
```

Compile and run the program.

### 12. Create HiltonMember Class
Repeat steps 8-10 to create `HiltonMember` class:

```csharp
public class HiltonMember : Member, IHiltonMember
{
    public string HiltonOnly { get; set; } = "This is Hilton only information";
    public override string[] Benefits { get; set; }
    
    public override string ToString() => 
        $"{base.ToString()}\n  Benefits: {string.Join(", ", Benefits)}";
    
    public new static class Factory
    {
        public static IHiltonMember CreateRandom()
        {
            var member = Member.Factory.CreateRandom();
            var hiltonMember = new HiltonMember(member)
            {
                Hotel = "Hilton",
                HiltonOnly = "This is Hilton only information",
                Benefits = "H:Free breakfast, H:Room upgrade, H:Free parking".Split(',')
            };
            return hiltonMember;
        }
    }
    
    public HiltonMember(IMember src) : base(src) {}
}
```

## Final Implementation

### 13. Update MemberList with Polymorphic Members
Update `Program.cs` to create a list alternating between Radisson and Hilton members:

```csharp
Console.WriteLine("\nCreate a MemberList of Radisson and Hilton members");
IMemberList memberList = new MemberList();
for (int i = 0; i < 10; i++)
{
    memberList.Add(RadissonMember.Factory.CreateRandom());
    memberList.Add(HiltonMember.Factory.CreateRandom());
}
Console.WriteLine(memberList);
```

**Key point:** The list is of type `IMemberList` containing `IMember` items, demonstrating polymorphism.

### 14. Complete Program.cs
Update `Main()` with complete demonstration:

```csharp
Console.WriteLine("Create Radisson and Hilton Members");
IMember myRadisson = RadissonMember.Factory.CreateRandom();
Console.WriteLine(myRadisson);

IMember myHilton = HiltonMember.Factory.CreateRandom();
Console.WriteLine(myHilton);

// Generic Member will show default "Nothing" benefits
Console.WriteLine("\nCreate a generic Member");
IMember genericMember = Member.Factory.CreateRandom();
Console.WriteLine(genericMember);

// Polymorphism: Benefits display correctly based on actual type
Console.WriteLine("\nCreate a MemberList of Radisson and Hilton members");
IMemberList memberList = new MemberList();
for (int i = 0; i < 10; i++)
{
    memberList.Add(RadissonMember.Factory.CreateRandom());
    memberList.Add(HiltonMember.Factory.CreateRandom());
}
Console.WriteLine(memberList);
```

**Important:** All variables use interface types (`IMember`, not `IRadissonMember` etc.), but Benefits are written correctly due to polymorphism.

Compile and run.

## Key Learning Points

### Object-Oriented Programming
1. **Inheritance**: Classes inherit from base classes (`Member`) and implement interfaces
2. **Polymorphism**: Using `virtual` and `override` keywords for polymorphic behavior
3. **Interface Inheritance**: Interfaces can inherit from other interfaces
4. **Factory Pattern**: Each derived class has its own factory leveraging the base class factory
5. **Copy Constructors**: Using `: base(src)` to call base class copy constructor
6. **Type Abstraction**: Working with interface types while actual objects are concrete implementations

### Polymorphism in Action
- Variables declared as `IMember` can hold `Member`, `RadissonMember`, or `HiltonMember` instances
- Benefits property displays correctly based on actual object type, not declared type
- `ToString()` method demonstrates polymorphic behavior through inheritance chain

## Practice Goals

Master these concepts through repetition:
- Creating inheritance hierarchies with multiple levels
- Implementing polymorphic behavior with virtual/override
- Using interface inheritance effectively
- Factory pattern in derived classes

**Keep practicing until these patterns become second nature!**
