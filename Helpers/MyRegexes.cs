using System.Text.RegularExpressions;

namespace AE1;

internal static partial class MyRegexes
{
	[GeneratedRegex(@"x\^(\d+)")]
	public static partial Regex PowRegex();

	[GeneratedRegex(@"^[-+*/^0-9.xX]*$")]
	public static partial Regex MathFunctionRegex();

	[GeneratedRegex(@"^-?[0-9]+$")]
	public static partial Regex IntNumberRegex();

	[GeneratedRegex(@"^[1-9]+[0-9]*$")]
	public static partial Regex PositiveIntNumberRegex();

	[GeneratedRegex(@"^[0-9]*\.?[0-9]+$")]
	public static partial Regex PositiveFloatNumberRegex();
}