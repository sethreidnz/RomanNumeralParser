using System;
using FluentAssertions;
using Xunit;

namespace RomanNumeralParser.Tests
{
    public class RomanNumeralParserTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("/")]
        [InlineData("1")]
        [InlineData("abcd")]
        [InlineData("$")]
        // TODO: add a library such as AutoFixture where I could generate random invalid strings within defined constraints
        public void ToInt_GivenNonRomanNumeralCharactersInput_Should_ThrowArgumentException(string input)
        {
            // arrange
            var sut = new RomanNumeralParser();

            // act
            Func<int> act = () => sut.ToInt(input);

            // assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"The input provided <{input}> is not a valid roman numeral.");
        }

        [Theory]
        [InlineData("IL")] // I and L are more than 2 away from each other
        [InlineData("LM")] // I and L are more than 2 away from each other
        [InlineData("IIX")] // only allowed one subtractive, should be VIII
        [InlineData("IIL")] // only allowed one subtractive, should be VIII
        // TODO: use a library or create function to generate random valid roman numerals for better coverage
        public void ToInt_GivenInvalidRomanNumeralInput_Should_ThrowArgumentException(string input)
        {
            // arrange
            var sut = new RomanNumeralParser();

            // act
            Func<int> act = () => sut.ToInt(input);

            // assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"The input provided <{input}> is not a valid roman numeral.");
        }

        [Theory]
        [InlineData("I", 1)]
        [InlineData("V", 5)]
        [InlineData("X", 10)]
        [InlineData("L", 50)]
        [InlineData("C", 100)]
        [InlineData("D", 500)]
        [InlineData("M", 1000)]
        [InlineData("IV", 4)]
        [InlineData("IX", 9)]
        [InlineData("XLIX", 49)]
        [InlineData("XIIII", 14)]
        [InlineData("MCMXCIX", 1999)]
        [InlineData("XXXII", 32)]
        [InlineData("LXV", 65)]
        [InlineData("XCVIII", 98)]
        [InlineData("LXXXIV", 84)]
        [InlineData("XCVI", 96)]
        [InlineData("CCXCVIII", 298)]
        [InlineData("DCCCLXXXVI", 886)]
        [InlineData("DCXXVII", 627)]
        public void ToInt_GivenValidRomanNumeralInput_Should_ReturnExpectedResult(string input, int expectedResult)
        {
            // arrange
            var sut = new RomanNumeralParser();

            // act
            var result = sut.ToInt(input);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("i", 1)]
        [InlineData("v", 5)]
        [InlineData("x", 10)]
        [InlineData("l", 50)]
        [InlineData("c", 100)]
        [InlineData("d", 500)]
        [InlineData("m", 1000)]
        [InlineData("iv", 4)]
        [InlineData("ix", 9)]
        [InlineData("xlix", 49)]
        public void ToInt_GivenLowerCase_ButValidRomanNumeralInput_Should_ReturnExpectedResult(string input, int expectedResult)
        {
            // arrange
            var sut = new RomanNumeralParser();

            // act
            var result = sut.ToInt(input);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(" I ", 1)]
        [InlineData(" V ", 5)]
        [InlineData(" X", 10)]
        [InlineData("L  ", 50)]
        [InlineData(" C  ", 100)]
        [InlineData(" D", 500)]
        [InlineData(" M", 1000)]
        public void ToInt_InputContainsWhitespace_ButIsOtherwiseAValidRomanNumeral_Should_ReturnExpectedResult(string input, int expectedResult)
        {
            // arrange
            var sut = new RomanNumeralParser();

            // act
            var result = sut.ToInt(input);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}