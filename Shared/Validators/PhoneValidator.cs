using System.Text.RegularExpressions;

namespace Shared.Validators;

public static class PhoneValidator
{
    public static string RemoveWhiteSpaces(string phoneNumber)
    {
        return Regex.Replace(phoneNumber, @"\s+", string.Empty);
    }
}
