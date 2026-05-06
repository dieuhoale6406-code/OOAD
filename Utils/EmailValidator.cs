namespace OOAD.Utils
{
    public static class EmailValidator
    {
        private static readonly System.Text.RegularExpressions.Regex _emailRegex =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                System.Text.RegularExpressions.RegexOptions.Compiled |
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return _emailRegex.IsMatch(email.Trim());
        }

        public static IEnumerable<string> ParseAndFilter(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return Enumerable.Empty<string>();

            var separators = new[] { ',', ';', '\r', '\n', '\t', ' ' };

            return raw
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim().ToLowerInvariant())
                .Where(IsValid)
                .Distinct(StringComparer.OrdinalIgnoreCase);
        }
    }
}