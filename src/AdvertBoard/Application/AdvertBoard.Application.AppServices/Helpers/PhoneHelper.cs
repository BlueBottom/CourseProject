using System.Text.RegularExpressions;

namespace AdvertBoard.Application.AppServices.Helpers;

public class PhoneHelper
{
    public static string NormalizePhoneNumber(string phoneNumber)
    {
        // Удаляем все лишние символы
        string cleanedNumber = Regex.Replace(phoneNumber, @"[^\d]", "");

        // Если номер начинается с +7, заменяем его на 8
        if (cleanedNumber.StartsWith("7"))
        {
            cleanedNumber = "8" + cleanedNumber.Substring(1);
        }

        return cleanedNumber;
    }
}