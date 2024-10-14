namespace AdvertBoard.Application.AppServices.Helpers;

/// <summary>
/// Вспомогательный класс для генерации кодов.
/// </summary>
public class CodeGeneratorHelper
{
    /// <summary>
    /// Генерирует OTP код.
    /// </summary>
    /// <returns>Код в виде <see cref="string"/>.</returns>
    public static string GenerateOtp()
    {
        const string characters = "1234567890";
        string otp = string.Empty;
        Random random = new Random();

        // Generate a 6-digit one-time password (OTP)
        for (int i = 0; i < 6; i++)
        {
            int index = random.Next(0, characters.Length);
            otp += characters[index];
        }

        return otp;
    }
}