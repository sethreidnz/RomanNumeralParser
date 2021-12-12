# Roman Number Parser

This repository contains a C# class called `RomanNumeralParser` contains a method `ToInteger` that takes a string input and returns the value as an integer if the input is valid.

## Prerequisites

In order to run the project locally you will need the following installed:

- [.NET Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Getting the code

To get the started ensure you have the requirements listed in the prerequisites section and then run the following to clone it to your local machine:

```bash
git clone https://github.com/sethreidnz/RomanNumeralParser
```

## Running the tests

Once you have cloned the repo run the following commands from the root of the projecT:

```bash
cd src
dotnet test
```

## How does it work?

This is a pretty simple implementation to calculate the values but it relies on validating the input first.

### Validation

The input is trimmed and converted to uppercase so any string that contains only whitespace and a valid combination of the roman numbers I, V, X, L, C, D, M is allowed.

Then there is regex validation done to prevent invalid combinations of those characters based on the rules of roman numerals. While I could have done this in some way by manually analyzing the string itself I opted to use a regex and write test to make sure that the regex was working as expected. This was for a few reasons:

- It made the actual calculation much more simple in both understandability and execution
- This is the kind of problem regex is made for - A predefined combination of characters with defined rules

The actual regex I took from [Oreilly's regular expression cookbook](https://www.oreilly.com/library/view/regular-expressions-cookbook/9780596802837/ch06s09.html) since it seemed thorough and well documented. I chose the "Modern Roman numerals, flexible:" because of the instruction to consider Postel's law and wanting to make it as permissible as possible. For example things like XIIII are actually invalid but still calculable by my algorithm so I chose this regex that would allow that. But the regex (and my tests) still stop invalid strings that look like roman numerals but would break my code e.g. IIIX instead of VII

### The algorithm to calculate the result

The algorithm itself is quite simple using a for loop that checks ahead using `i + 1` to see if I need to subtract the value of the current symbol. If the next symbol has a higher value than the current number then it needs to be subtracted. Otherwise it will be added.

Since the rules of roman numerals mean that you cannot do more then on subtraction and I've already validated the string as a valid Roman Numeral using regex I can be confident in a simple algorithm like this.

## Limitations/thoughts

Below are some limitations:

- Could have made this an extension method rather than a class. Was an arbitrary decision since the context of the application is unclear.
- Non-exhaustive tests suite - In reality I would use a library like AutoFixture and build a way to generate random string and roman numerals for more chaos testing - However I tried to cover the edge cases in my inputs
- Does not take into account the multiplier symbols uses with the line across the top. This was just due to time and only realizing this was a thing at the end.
- Potentially there may be a more efficient way than using a for loop but I went for understandability since it's a roman numeral and it's only going to be so long
