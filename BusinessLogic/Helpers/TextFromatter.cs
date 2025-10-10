namespace BusinessLogic.Helpers
{
    public static class TextFromatter
    {
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            return char.ToUpperInvariant(input[0]) + input.Substring(1).ToLowerInvariant();
        }
    }
}
