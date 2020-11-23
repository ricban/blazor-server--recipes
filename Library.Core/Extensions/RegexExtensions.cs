using System.Text.RegularExpressions;

namespace Library.Core.Extensions
{
    public static class RegexExtensions
    {
        public static bool IsMatch(this string value, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            return Regex.IsMatch(value, pattern, options);
        }

        public static Match Match(this string value, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            return Regex.Match(value, pattern, options);
        }
    }
}
