namespace indiGo.Core.Extensions;

public static class StringExtensions
{
    public static string CapitalizeFirstLetter(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return str.Substring(0, 1).ToUpper() +
               str.Substring(1);
    }
}