# Kata05: Delegates & Lambda Functions

## Overview
This kata builds on Kata02 and focuses on practicing delegates and lambda functions in C#. You'll learn how to use delegates for callbacks, work with lambda expressions, and understand functional programming concepts.

## Project Setup

### 1. Create New Project
Create a new project named `Kata05_Delegates_Lamda`. Copy all code files from Kata02 into the new project.

Compile and run to ensure you have a stable starting point.

### 2. Update Namespace
Change the namespace in all files to `Kata05_Delegates_Lamda`. Compile and run.

## Working with Delegates

### 3. Add Delegate Parameter to Factory
Modify `MemberList.Factory.CreateRandom()` to accept a delegate that processes each new member:

```csharp
public static class Factory
{
    public static MemberList CreateRandom(int NrOfItems, Func<IMember, IMember> NewMemberAction = null)
    {
        var memberlist = new MemberList();
        for (int i = 0; i < NrOfItems; i++)
        {
            var newMember = Member.Factory.CreateRandom();
            if (NewMemberAction != null)
            {
                newMember = NewMemberAction(newMember);
                if (newMember == null)
                    throw new Exception("NewMemberAction returned null");
            }
            memberlist._members.Add(newMember);
        }
        return memberlist;
    }
}
```

**Key features:**
- Parameter: `Func<IMember, IMember> NewMemberAction` - a delegate that takes an `IMember` and returns an `IMember`
- Optional parameter with default value `null`
- Calls the delegate after creating each member
- Returns the potentially modified member
- Validates that the delegate doesn't return `null`

**Benefits of this approach:**
- The calling code decides what to do with each new member
- Separation of concerns - list creation vs. member processing
- Flexible and reusable - different actions for different lists
- Classic example of the callback pattern

### 4. Create Delegate Methods for Greetings
In `Program.cs`, create static methods that match the `Func<IMember, IMember>` signature:

```csharp
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
```

**Method signature requirements:**
- Must match `Func<IMember, IMember>` - takes `IMember` parameter and returns `IMember`
- Can modify the member (set Hotel property)
- Can perform side effects (write to console)
- Must return the member (or a modified version)

### 5. Use Delegate Methods
Call the factory with delegate methods:

```csharp
Console.WriteLine("\nCreate a 20 Hilton members");
var HiltonMembers = MemberList.Factory.CreateRandom(20, HelloHilton); 
HiltonMembers.Sort();
Console.WriteLine(HiltonMembers);

Console.WriteLine("\nCreate a 20 Radisson members");
var RadissonMembers = MemberList.Factory.CreateRandom(20, HelloRadisson);
RadissonMembers.Sort();
Console.WriteLine(RadissonMembers);
```

**Result:** Each member gets a personalized greeting as it's created, and the Hotel property is set accordingly.

Compile and run.

## Using Lambda Functions

### 6. Replace Delegate Methods with Lambda Expressions
Instead of separate delegate methods, you can use lambda functions directly in the method call. This also allows you to **capture** (close over) variables from the surrounding scope.

Create a Scandic members list using a lambda expression:

```csharp
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
```

**Key features:**
- Lambda syntax: `m => { ... }`
- Parameter `m` represents the `IMember` being processed
- **Closure**: The lambda captures `nrBlueScandic` from outer scope
- Increments the counter when encountering Blue level members
- Sets the Hotel property for each member
- Returns the modified member

**Advantages of lambda over delegate methods:**
- More concise - no need for separate method declaration
- Can capture local variables (closures)
- Code is right where it's used - better readability for simple operations
- Ideal for one-time use cases

Compile and run.

## Filtering with Delegates

### 7. Add Filter Method to MemberList
Add a filtering capability using a `Func<IMember, bool>` predicate:

```csharp
public IEnumerable<IMember> Filter(Func<IMember, bool> predicate) => 
    _members.FindAll(m => predicate(m));
```

**Implementation details:**
- Takes a predicate delegate: `Func<IMember, bool>` - returns `true` if member matches criteria
- Uses `List<T>.FindAll()` to find all matching members
- Returns `IEnumerable<IMember>` - allows iteration over results
- Lambda `m => predicate(m)` applies the predicate to each member

### 8. Create Predicate Delegate Method
Create a method that filters for Gold members:

```csharp
static bool IsGold(IMember member) => member.Level == MemberLevel.Gold;
```

**Signature:** `Func<IMember, bool>` - takes `IMember` and returns `bool`

### 9. Use Filter with Both Delegate and Lambda
Test filtering with both approaches:

```csharp
// Using delegate method
Console.WriteLine($"HiltonMembers Gold Members: {HiltonMembers.Filter(IsGold)}");

// Using lambda expression
Console.WriteLine($"RadissonMembers Gold Members: {RadissonMembers.Filter(m => m.Level == MemberLevel.Gold)}");
```

**Comparison:**
- **Delegate method** (`IsGold`): Reusable, named, good for complex logic or multiple uses
- **Lambda expression**: Inline, concise, perfect for simple one-off filters

Compile and run.

## Complete Program Example

```csharp
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
```

## Key Learning Points

### Delegates
1. **Callback Pattern**: Pass behavior as a parameter to enable flexible, reusable code
2. **Func<T, TResult>**: Built-in delegate type for methods that return a value
3. **Action<T>**: Built-in delegate type for void methods (not used here, but important to know)
4. **Separation of Concerns**: List creation logic is separate from member processing logic
5. **Flexibility**: Different delegate methods can be passed for different scenarios

### Lambda Expressions
1. **Syntax**: `parameter => expression` or `parameter => { statements; return value; }`
2. **Closures**: Lambda functions can capture variables from surrounding scope
3. **Inline Code**: Define behavior right where it's used
4. **Type Inference**: Compiler infers parameter types from context
5. **Expression vs. Statement**: Single expressions don't need braces or return; multi-statement lambdas do

### Functional Programming Concepts
1. **First-Class Functions**: Functions can be passed as parameters and stored in variables
2. **Higher-Order Functions**: Functions that take other functions as parameters (like `CreateRandom` and `Filter`)
3. **Predicates**: Functions that return boolean values for filtering/testing
4. **Side Effects**: Functions can modify state or perform I/O (like console output)

### Practical Applications
- **Event handlers**: Delegates are the foundation of C# events
- **LINQ**: Heavily uses lambda expressions for filtering, mapping, and aggregating
- **Async programming**: Callbacks and continuations
- **Strategy pattern**: Pass different behaviors to the same method
- **Template Method pattern**: Define algorithm structure, vary specific steps

## Delegate vs Lambda Decision Guide

**Use Named Delegate Methods when:**
- Logic is complex or multi-line
- Method is reused in multiple places
- You want meaningful method names for documentation
- Debugging needs to be easier (named methods in stack traces)

**Use Lambda Expressions when:**
- Logic is simple and concise
- One-time use case
- Need to capture local variables (closures)
- Code readability benefits from inline definition
- Working with LINQ or similar functional APIs

## Practice Goals

Master these concepts through repetition:
- Writing methods that accept delegate parameters
- Creating delegate methods with correct signatures
- Using lambda expressions for simple callbacks
- Understanding closures and variable capture
- Applying predicates for filtering
- Choosing between delegates and lambdas appropriately

**Keep practicing until delegates and lambdas become natural tools in your programming toolkit!**
