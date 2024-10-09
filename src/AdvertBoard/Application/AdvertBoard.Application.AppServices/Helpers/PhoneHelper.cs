using System.Text.RegularExpressions;

namespace AdvertBoard.Application.AppServices.Helpers;

/// <summary>
/// Класс с вспомогательными методами нормализации.
/// </summary>
public class PhoneHelper
{
    /// <summary>
    /// Нормализует номер телефона.
    /// </summary>
    /// <param name="phoneNumber">Номер телефона.</param>
    /// <returns>Номер телефона, приведенный к единому виду.</returns>
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