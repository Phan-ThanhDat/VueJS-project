using System;

namespace Aibidia.Common.Extensions
{
    public static class StringExtensions
    {
        public const string Quote = "\"";

        public static string Wrap(this String str, String wrapChars)
        {
            return $"{wrapChars}{str}{wrapChars}";
        }

        public static string WrapInQuotes(this String str)
        {
            return Wrap(str, Quote);
        }

        public static string UnWrap(this String str, String wrapChars)
        {
            if (wrapChars == null) throw new ArgumentNullException(nameof(wrapChars), "Wrapping characters can't be null.");

            if (str == null) return str;

            if (str.StartsWith(wrapChars) && str.EndsWith(wrapChars) && str.Length >= 2*wrapChars.Length)
            {
                // Return the string in between the wrapping characters:
                //   "abc"   (Length: 5)
                //   01234   --> actual string starts in position 1 and the length is the same as the original string minus starting and ending wrappers.
                //
                //   ***abc*** (Length: 9)
                //   012345678  --> actual string starts in position 3 and its length is 9-6 = 3
                //
                //   abc (Length: 3)  --> The string starts and ends with the wrapping chars.
                return str.Substring(wrapChars.Length, str.Length - 2 * wrapChars.Length);
            }
            else
            {
                // Nothing to unwrap
                return str;
            }
        }
    }
}
