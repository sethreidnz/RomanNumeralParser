using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RomanNumeralParser
{
    public class RomanNumeralParser
    {
        private static readonly Dictionary<char, int> RomanNumeralMap = new()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        // source: https://www.oreilly.com/library/view/regular-expressions-cookbook/9780596802837/ch06s09.html
        private const string RomanNumberValidationRegEx = "^(?=[MDCLXVI])M*(C[MD]|D?C*)(X[CL]|L?X*)(I[XV]|V?I*)$";

        /// <summary>
        /// Converts a string Roman numeral to an integer. Performs validation to ensure it is a valid roman numeral  but
        /// ignores whitespace and is case-insensitive
        /// </summary>
        /// <param name="input">The string input</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public int ToInt(string input)
        {
            var romanNumeral = input.Trim().ToUpper();
            if (string.IsNullOrEmpty(romanNumeral) ||
                !Regex.IsMatch(romanNumeral, RomanNumberValidationRegEx))
            {
                throw new ArgumentException($"The input provided <{input}> is not a valid roman numeral.");
            }

            var value = 0;
            for (var i = 0; i < romanNumeral.Length; i++)
            {
                var currentChar = romanNumeral[i];
                var nextChar = i + 1 < romanNumeral.Length ? romanNumeral[i + 1] : currentChar;
                var currentCharValue = RomanNumeralMap[currentChar];
                var nextCharValue = RomanNumeralMap[nextChar];
                if (nextCharValue > currentCharValue)
                {
                    value -= currentCharValue;
                }
                else
                {
                    value += currentCharValue;
                }
            }
            return value;
        }
    }
}