namespace AE1.Helpers;

internal static class NCalcHelpers
{
    public static string PreprocessEquation(string equation)
    {
        // Replace '^' with 'Pow'
        equation = MyRegexes.PowRegex().Replace(equation, match => $"Pow(x, {match.Groups[1].Value})");
        // Replace 'x' with '[x]' (needed for substitution of parameter)
        return equation.Replace("X", "x").Replace("x", "[x]");
    }
}