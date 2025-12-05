# Kata04: Immutability

## Overview
This kata builds on Kata02 and focuses on practicing immutability in C#. You'll learn to create immutable classes using both traditional class approaches and the modern record type.

## Project Setup

### 1. Create New Project
Create a new project named `Kata04_Immutability`. Copy all code files from Kata02 into the new project.

Compile and run to ensure you have a stable starting point.

### 2. Update Namespace
Change the namespace in all files to `Kata04_Immutability`. Compile and run.

## Making IMember Immutable

### 3. Update IMember Interface
Modify the `IMember` interface to support immutability:

**Change all properties from `set` to `init`:**
```csharp
public interface IMember : IEquatable<IMember>, IComparable<IMember>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public MemberLevel Level { get; init; }
    public DateTime Since { get; init; }
    public string Hotel { get; init; }
}
```

**Key points:**
- `init` accessors allow property values to be set only during object initialization
- External code cannot modify properties after creation
- The interface requires `IEquatable<IMember>` and `IComparable<IMember>` implementation

### 4. Rename Member to ImmClassMember
Rename both the class and the file from `Member` to `ImmClassMember`.

## Creating an Immutable Class

### 5. Make ImmClassMember Immutable
Update the `ImmClassMember` class for immutability:

```csharp
public class ImmClassMember : IMember
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public MemberLevel Level { get; init; }
    public DateTime Since { get; init; }
    public string Hotel { get; init; } = "No Hotel";
    
    public override string ToString() => 
        $"{FirstName} {LastName} is a {Hotel} {Level} member since {Since.Year}";
}
```

**Implementation steps:**
1. Keep `IEquatable<IMember>` implementation (the interface requires it)
2. Change all properties to use `init` instead of `set`
3. Properties can now only be set during object initialization

### 6. Ensure Copy Constructor Exists
Add or verify the copy constructor:

```csharp
public ImmClassMember(ImmClassMember src)
{
    this.FirstName = src.FirstName;
    this.LastName = src.LastName;
    this.Level = src.Level;
    this.Since = src.Since;
    this.Hotel = src.Hotel;
}
```

**Note:** Copy constructor is essential for creating modified copies of immutable objects.

### 7. Create Value Change Methods (Fluent Syntax)
Add methods that return new instances with modified values:

```csharp
#region Value change methods in an immutable class
public ImmClassMember SetFirstName(string name) => new ImmClassMember(this) { FirstName = name };
public ImmClassMember SetLastName(string name) => new ImmClassMember(this) { LastName = name };
public ImmClassMember SetLevel(MemberLevel level) => new ImmClassMember(this) { Level = level };
#endregion
```

**How it works:**
1. Create a copy using the copy constructor
2. Set the property value in the copy using `init`
3. Return the new instance

**Key insight:** The original instance is never modified - each change creates a new object.

### 8. Test Immutable Class
In `Program.cs`, test the immutable class:

```csharp
var member1 = ImmClassMember.Factory.CreateRandom();
Console.WriteLine($"member1: {member1}");

var newMember1 = member1.SetFirstName("Karl").SetLastName("Petterson").SetLevel(MemberLevel.Platinum);
Console.WriteLine($"Modified Member: {newMember1}");
Console.WriteLine($"Original member1 unchanged: {member1}");
```

**Important:** Due to immutability, each `Set` method call returns a new instance. `member1` remains unchanged.

Compile and run.

## Creating an Immutable Record

**Observation:** Creating immutable classes requires significant boilerplate code. C# records provide a more concise approach.

### 9. Create ImmRecordMember
Create a new file `ImmRecordMember.cs` with a record type:

```csharp
public record ImmRecordMember(string FirstName, string LastName, MemberLevel Level, 
                              DateTime Since, string Hotel) : IMember
{
    #region Implement IComparable
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
    
    // Only needed because of IEquatable<IMember> interface requirement
    public bool Equals(IMember other) => this == (ImmRecordMember)other;
    #endregion

    public override string ToString() => 
        $"{FirstName} {LastName} is a {Level} member since {Since.Year}";
}
```

**What records provide automatically:**
- Properties with `{ get; init; }` accessors from parameters
- Copy constructor (protected)
- `Equals()` and `GetHashCode()` based on property values (but you need explicit `Equals(IMember)` for interface)
- `with` expression support for creating modified copies
- Deconstruction support
- `==` and `!=` operators for value equality

### 10. Simplify Record Code
Records eliminate the need for:
- ❌ Explicit property declarations (defined in primary constructor)
- ❌ Property `Set` methods (use `with` expression instead)
- ❌ Manual copy constructor (compiler-generated)
- ❌ Default constructor (use primary constructor)
- ❌ `IEquatable` implementation (automatic value-based equality)

**You only need:**
- ✅ `IComparable` implementation (business logic specific)
- ✅ `Equals(IMember)` implementation (for interface requirement)
- ✅ `ToString()` override (optional - records have a default implementation)
- ✅ Class factory

### 11. Update Factory for Record
Modify the factory to use the primary constructor:

```csharp
public static class Factory
{
    public static ImmClassMember CreateRandom()
    {
        var rnd = new Random();
        var Level = (MemberLevel)rnd.Next((int)MemberLevel.Platinum, (int)MemberLevel.Blue + 1);
        
        var _fnames = "Harry, Lord, Hermione, Albus, Severus, Ron, Draco, Frodo, Gandalf, Sam, Peregrin, Saruman".Split(", ");
        var _lnames = "Potter, Voldemort, Granger, Dumbledore, Snape, Malfoy, Baggins, the Gray, Gamgee, Took, the White".Split(", ");
        var FirstName = _fnames[rnd.Next(0, _fnames.Length)];
        var LastName = _lnames[rnd.Next(0, _lnames.Length)];
        
        var startDate = new DateTime(1980, 1, 1);
        var endDate = DateTime.Today;
        int range = (endDate - startDate).Days;
        var randomDays = rnd.Next(range);
        var Since = startDate.Add(TimeSpan.FromDays(randomDays));
        
        // Use primary constructor instead of object initialization
        var member = new ImmRecordMember(FirstName, LastName, Level, Since, "No Hotel");
        return member;
    }
}
```

### 12. Test Immutable Record
In `Program.cs`, test the record with the `with` expression:

```csharp
var member2 = ImmRecordMember.Factory.CreateRandom();
Console.WriteLine($"member2: {member2}");

var newMember2 = member2 with { FirstName = "Karl", LastName = "Petterson" };
Console.WriteLine($"Modified Member: {newMember2}");
Console.WriteLine($"Original member2 unchanged: {member2}");
```

**Key feature:** The `with` expression creates a copy with specified properties changed. No need for custom `Set` methods!

Compile and run.

## Working with Mixed Immutable Types

### 13. Polymorphic Immutable List
Both `ImmClassMember` and `ImmRecordMember` implement `IMember`, so they can coexist in the same collection:

```csharp
Console.WriteLine("\nCreate a 20 Hotel members");
IMemberList hotelMembers = new MemberList();
for (int i = 0; i < 20; i++)
{
    hotelMembers.Add(ImmClassMember.Factory.CreateRandom());
    hotelMembers.Add(ImmRecordMember.Factory.CreateRandom());
}
Console.WriteLine(hotelMembers);
```

### 14. Deep Copy with Mixed Types
Update `MemberList` copy constructor to handle both types:

```csharp
public MemberList(MemberList org)
{
    // Reference Copy (commented out - not recommended)
    // _members = org._members;
    
    // Shallow Copy (commented out - not a true deep copy)
    // _members = new List<IMember>(org._members);
    
    // Deep copy using LINQ - Records use 'with' expression for copying
    _members = org._members
        .Select<IMember, IMember>(o => o is ImmRecordMember r 
            ? r with { }  // Use 'with' for records
            : new ImmClassMember((ImmClassMember)o))  // Use copy constructor for classes
        .ToList();
}
```

**Key points:**
- Records: Use `with { }` expression (creates a copy with no changes)
- Classes: Use copy constructor
- Type checking with pattern matching (`is ImmRecordMember r`)

## Complete Program Example

```csharp
Console.WriteLine("Create a couple of immutable members");

var member1 = ImmClassMember.Factory.CreateRandom();
Console.WriteLine($"\nmember1 of type {member1.GetType().Name}: {member1}");

var newMember1 = member1.SetFirstName("Karl").SetLastName("Petterson").SetLevel(MemberLevel.Platinum);
Console.WriteLine($"Modified Member from immutable member1: {newMember1}");

var member2 = ImmRecordMember.Factory.CreateRandom();
Console.WriteLine($"\nmember2 of type {member2.GetType().Name}: {member2}");

var newMember2 = (ImmRecordMember)member2 with { FirstName = "Karl", LastName = "Petterson" };
Console.WriteLine($"Modified Member from immutable member2: {newMember2}");

Console.WriteLine("\nCreate a 20 Hotel members");
IMemberList hotelMembers = new MemberList();
for (int i = 0; i < 20; i++)
{
    hotelMembers.Add(ImmClassMember.Factory.CreateRandom());
    hotelMembers.Add(ImmRecordMember.Factory.CreateRandom());
}
Console.WriteLine(hotelMembers);
```

## Key Learning Points

### Immutability Concepts
1. **Immutability**: Objects cannot be modified after creation
2. **init Accessors**: Properties can only be set during object initialization
3. **Value Change Methods**: Return new instances instead of modifying existing ones
4. **Fluent Syntax**: Chain multiple changes together
5. **Thread Safety**: Immutable objects are inherently thread-safe

### Class vs Record Comparison

| Feature | Immutable Class | Immutable Record |
|---------|----------------|------------------|
| Property Declaration | Manual with `{ get; init; }` | Automatic from parameters |
| Equality | Manual `IEquatable` implementation | Automatic value-based equality |
| Copy Constructor | Manual implementation | Compiler-generated (protected) |
| Value Changes | Custom `Set` methods | `with` expression |
| Code Volume | More boilerplate | Concise |
| Best For | Complex business logic | Data transfer objects, simple entities |

### When to Use Records
- ✅ Data-centric types
- ✅ Value semantics needed
- ✅ Immutability by default
- ✅ Simple data structures
- ✅ Minimal boilerplate desired

### When to Use Classes
- ✅ Complex behavior and methods
- ✅ Need mutable internal state
- ✅ Reference equality desired
- ✅ Inheritance hierarchies

## Practice Goals

Master these concepts through repetition:
- Creating immutable classes with `init` properties
- Implementing fluent-style value change methods
- Using records for concise immutable types
- Working with `with` expressions
- Handling mixed immutable types in collections
- Understanding value vs reference semantics

**Great work! You've now mastered immutability using both traditional classes and modern C# records!**
