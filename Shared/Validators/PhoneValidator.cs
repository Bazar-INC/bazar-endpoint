using System.Text.RegularExpressions;

namespace Shared.Validators;

public static class PhoneValidator
{
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        const int phoneNumberLenght = 9; // 67 363 00 11

        if (phoneNumber.Length != phoneNumberLenght)
        {
            return false;
        }

        return phoneNumber.All(x => char.IsDigit(x));
    }

    public static string RemoveWhiteSpaces(string phoneNumber)
    {
        return Regex.Replace(phoneNumber, @"\s+", string.Empty);
    }
}
