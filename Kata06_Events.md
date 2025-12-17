# Kata06: Events

## Overview
This kata builds on Kata05 and focuses on practicing C# events. You'll learn how to declare events, raise them, and subscribe event handlers to respond to specific occurrences in your application.

## Project Setup

### 1. Create New Project
Create a new project named `Kata06_Events`. Copy all code files from Kata05 into the new project.

Compile and run to ensure you have a stable starting point.

### 2. Update Namespace
Change the namespace in all files to `Kata06_Events`. Compile and run.

## Understanding Events

Events in C# implement the **Observer pattern**, allowing objects to notify other objects when something happens without tight coupling. Events are based on delegates but with added safety - only the declaring class can invoke the event.

**Key Concepts:**
- **Publisher**: The class that declares and raises the event
- **Subscriber**: The class/method that handles the event
- **Event Handler**: A delegate method that responds to the event
- **Event Args**: Data passed to event handlers (can be custom types)

## Implementing Events

### 3. Add Events to MemberList

Declare event handlers and methods to raise events in the `MemberList` class:

```csharp
public class MemberList : IMemberList
{
    List<IMember> _members = new List<IMember>();
    
    // Event handler declarations
    public EventHandler<IMember> PlatinumMemberEvent { get; set; }
    public EventHandler<IMember> GoldMemberEvent { get; set; }
    
    // Methods that raise the events
    public void OnPlatinumMember(IMember member) => PlatinumMemberEvent?.Invoke(this, member);
    public void OnGoldMember(IMember member) => GoldMemberEvent?.Invoke(this, member);
}
```

**Key implementation details:**
- **Event Type**: `EventHandler<IMember>` - generic event handler where `IMember` is the event argument type
- **Properties**: Auto-properties that allow subscribers to add/remove handlers
- **`On...` Methods**: Convention for methods that raise events
- **Null-conditional operator (`?.`)**: Safely invokes only if there are subscribers
- **`this` parameter**: The event sender (the `MemberList` instance)

### 4. Update IMemberList Interface

Add event declarations to the interface:

```csharp
public interface IMemberList
{
    public IMember this[int idx] { get; }  
    
    public int Count();
    public int Count(int year);
    public void Sort();
    public void Add(IMember member);
    
    public EventHandler<IMember> PlatinumMemberEvent { get; set; }
    public EventHandler<IMember> GoldMemberEvent { get; set; }
}
```

### 5. Raise Events When Adding Members

Modify the `Add` method to fire events based on member level:

```csharp
public void Add(IMember member)
{
    _members.Add(member);
    
    // Fire events based on member level
    if (member.Level == MemberLevel.Platinum)
        OnPlatinumMember(member);
    else if (member.Level == MemberLevel.Gold)
        OnGoldMember(member);
}
```

**When events fire:**
- Adding a Platinum member triggers `PlatinumMemberEvent`
- Adding a Gold member triggers `GoldMemberEvent`
- Silver and Blue members don't trigger events (but you could add more)

### 6. Create Event Handler Methods

In `Program.cs`, create static methods that match the `EventHandler<IMember>` signature:

```csharp
static void PlatinumMemberEventHandler(object sender, IMember member)
{
    Console.WriteLine($"#### {member.FirstName} {member.LastName} is a Platinum member! ####");
}

static void GoldMemberEventHandler(object sender, IMember member)
{
    Console.WriteLine($"#### {member.FirstName} {member.LastName} is a Gold member! ####");
}
```

**Event Handler Signature:**
- **First parameter**: `object sender` - the object that raised the event
- **Second parameter**: Event-specific data (here, `IMember`)
- **Return type**: `void` (event handlers don't return values)

### 7. Subscribe to Events

Subscribe event handlers before adding members:

```csharp
// Subscribe to events
HiltonMembers.PlatinumMemberEvent += PlatinumMemberEventHandler;
RadissonMembers.PlatinumMemberEvent += PlatinumMemberEventHandler;
RadissonMembers.GoldMemberEvent += GoldMemberEventHandler;

// Create new members to see events in action
Console.WriteLine("\nAdding new Hotel Members - Catching Gold and Platinum events:");
for (int i = 0; i < 20; i++)
{
    HiltonMembers.Add(Member.Factory.CreateRandom());
    RadissonMembers.Add(Member.Factory.CreateRandom());
}
```

**Subscription syntax:**
- Use `+=` to subscribe (add a handler)
- Use `-=` to unsubscribe (remove a handler)
- Multiple handlers can subscribe to the same event
- Same handler can subscribe to multiple events

Compile and run.

## Complete Program Example

```csharp
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

// Subscribe to events
HiltonMembers.PlatinumMemberEvent += PlatinumMemberEventHandler;
RadissonMembers.PlatinumMemberEvent += PlatinumMemberEventHandler;
RadissonMembers.GoldMemberEvent += GoldMemberEventHandler;

// Create new members to see events in action
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

#region Event Handlers
static void PlatinumMemberEventHandler(object sender, IMember member)
{
    Console.WriteLine($"#### {member.FirstName} {member.LastName} is a Platinum member! ####");
}

static void GoldMemberEventHandler(object sender, IMember member)
{
    Console.WriteLine($"#### {member.FirstName} {member.LastName} is a Gold member! ####");
}
#endregion
```

## Key Learning Points

### Events vs Delegates
| Aspect | Delegate | Event |
|--------|----------|-------|
| Invocation | Anyone with reference can invoke | Only declaring class can invoke |
| Assignment | Can be reassigned (`=`) | Only add/remove (`+=`, `-=`) |
| Safety | Less safe - can be cleared | Safer - maintains subscriber list |
| Use Case | Callbacks, function pointers | Notifications, Observer pattern |

### Event Best Practices
1. **Naming Convention**: 
   - Events: PascalCase ending with "Event" (e.g., `PlatinumMemberEvent`)
   - Event raisers: Prefix with "On" (e.g., `OnPlatinumMember`)

2. **Null Check**: 
   - Always use `?.Invoke()` to prevent `NullReferenceException` if no subscribers
   
3. **EventHandler\<T\>**: 
   - Use `EventHandler<T>` for events with custom data
   - Use `EventHandler` for events with no specific data (uses `EventArgs`)
   
4. **Encapsulation**: 
   - Events should typically be `public` for external subscription
   - Event raising methods (`On...`) can be `protected virtual` for inheritance scenarios

### Common Event Patterns

**Standard EventArgs Pattern:**
```csharp
public event EventHandler<MyCustomEventArgs> MyEvent;

public class MyCustomEventArgs : EventArgs
{
    public string Data { get; set; }
}
```

**Simplified Pattern (used in this kata):**
```csharp
public EventHandler<IMember> PlatinumMemberEvent { get; set; }
```

### When to Use Events
- ✅ **GUI Applications**: Button clicks, mouse movements, window events
- ✅ **Notifications**: System needs to notify multiple components
- ✅ **Loose Coupling**: Publisher doesn't need to know subscribers
- ✅ **Observer Pattern**: One-to-many dependency between objects
- ✅ **Asynchronous Operations**: Progress updates, completion notifications

### When NOT to Use Events
- ❌ Direct method calls are simpler and sufficient
- ❌ Only one subscriber expected (consider interfaces/delegates)
- ❌ Need return values from handlers (use delegates instead)
- ❌ Tight coupling is acceptable or desired

## Advanced Event Concepts

### Multiple Subscribers
```csharp
// Multiple handlers can subscribe to the same event
list.PlatinumMemberEvent += Handler1;
list.PlatinumMemberEvent += Handler2;
list.PlatinumMemberEvent += Handler3;
// All three will be called when event fires
```

### Unsubscribing
```csharp
// Always unsubscribe when done to prevent memory leaks
list.PlatinumMemberEvent -= PlatinumMemberEventHandler;
```

### Lambda Subscribers
```csharp
// Can use lambda expressions as event handlers
list.GoldMemberEvent += (sender, member) => 
{
    Console.WriteLine($"Gold member added: {member.FirstName}");
};
```

### Event Order
- Handlers are invoked in the order they were subscribed
- No guaranteed order - don't rely on execution sequence
- Each handler executes synchronously (one after another)

## Practical Applications

### Real-World Scenarios
1. **UI Events**: Button clicks, form submissions, menu selections
2. **Data Changes**: Notify UI when data model changes
3. **Logging**: Centralized event logging across application
4. **Validation**: Multi-stage validation with event-driven checks
5. **Workflow**: State change notifications in business processes
6. **Messaging**: Publish-subscribe patterns in distributed systems

### Benefits in This Kata
- **Separation of Concerns**: List management separate from notification logic
- **Extensibility**: Easy to add new event handlers without modifying `MemberList`
- **Testability**: Can verify events fire without full integration tests
- **Flexibility**: Different subscribers can react differently to same event

## Practice Goals

Master these concepts through repetition:
- Declaring events with appropriate types
- Implementing `On...` methods to raise events
- Creating event handler methods with correct signatures
- Subscribing and unsubscribing from events
- Understanding event lifecycle and null safety
- Applying Observer pattern in real scenarios
- Distinguishing between events and delegates

**Keep practicing until events become a natural part of your C# toolkit!**
