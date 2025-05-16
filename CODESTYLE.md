# ContentDotNet Code Style
This document describes how code should be written when contributing/maintaining ContentDotNet.

ContentDotNet is written in C#, so everything in this document applies to the C# programming language specifically.

## 1. Curly brackets
We prefer curly brackets to be placed on the new line (Allman).

❌ Don't do this:
```cs
if (something) {
    // ...
}
```

✅ Do this instead:
```cs
if (something)
{
    // ...
}
```

## 2. Naming
**1.** Don't use underscores, unless methods in unit tests.

❌ Don't do this
```cs
int my_number = 42;

class My_Class
{
}
```
✅ Do this instead
```cs
int myNumber = 42;

class MyClass
{
}
```

But in unit tests, methods like these are fine:
```cs
[Fact]
public void Input_Too_Long_Should_Throw()
{
    // ...
}
```

**2.** Use camelCase for variables and method parameters, _camelCaseWithUnderscore for private fields, PascalCase for methods, classes, and properties regardless of their access modifier, and PascalCase for non-private fields.

**3.** Convert three or more letter abbreviations into a capitalized form

e.g. don't use `JITCompiler`; use `JitCompiler` instead. Don't use `RTSPMessage`; use `RtspMessage` instead.

Two-letter abbreviations (e.g. `IO` for Input and Output) should be kept all uppercase (e.g. don't use `IoService`, use `IOService`).

## 3. Other
**1.** Don't make fields public (prefer properties), *unless* the scenario is highly performance critical.

Properties already consist of fields, but when you get/set them, it adds overhead of invoking a hidden auto-generated method that retrieves the field first. If the scenario is performance-critical to the point where even properties could potentially slow things down, be safer with public fields - such as when designing H.264-related structures that require constantly pulling unmanaged stack-allocated data many times.

**2.** Always indent with 4 spaces. Prefer spaces over tabs.

**3.** Always emit access modifiers.

For example, omitting an access modifier for a class leaves it `internal` by default, but you still have to specify the `internal` keyword for clarity.

**4.** Seal types if you're sure they don't need any inheritance

This improves performance.

**5.** Don't use abbreviations unless necessary

Don't make custom abbreviations that don't actually exist, but you should use the abbreviation for example, JIT, for "Just-In-Time compiler" because it's an official term.

**6.** Don't put a whitespace after method names, method invocations, start/end of parenthesized expressions, or explicit casting.

❌ Don't do this:
```cs
// 1. 🙅🏻
if ( someCondition )
{
}

// 2. 🙅🏻
MyMethod (1, 2, 3);

// 3. 🙅🏻
MyMethod ( 1, 2, 3 );

// 4. 🙅🏻
int integerFromSingle = (int) mySingle;

// 5. 🙅🏻
static void DoWork ()
{
}
```

✅ Do this instead
```cs
if (someCondition)
{
}

MyMethod(1, 2, 3);

MyMethod(1, 2, 3);

int integerFromSingle = (int)mySingle;

static void DoWork()
{
}
```

**7.** Place a whitespace after the `if`, `for`, `foreach`, or `while` keyword

❌ Don't do this
```cs
if(someCondition)
    Console.WriteLine("❌");

for(int i = 0; i < 5; i++)
    Console.WriteLine("❌");

foreach(string str in this.Collection)
    Console.WriteLine("❌");

while(true)
    Console.WriteLine("❌");
```

✅ Do this instead
```cs
if (someCondition)
    Console.WriteLine("✅");

for (int i = 0; i < 5; i++)
    Console.WriteLine("✅");

foreach (string str in this.Collection)
    Console.WriteLine("✅");

int i = 0;
while (i++ < 5)
    Console.WriteLine("✅");
```

**8.** Use `this` and `base`

While not strictly necessary, it can at least provide clarity in if the field/method/property being accessed is from the current class (`this`) or the inheriting class (`base`).
