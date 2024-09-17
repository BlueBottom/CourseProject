using System.Security.Cryptography;
using System.Text;

namespace AdvertBoard.Application.AppServices.Helpers;

/// <summary>
/// Класс с вспомогательными методами хеширования.
/// </summary>
public class CryptoHelper
{
    /// <summary>
    /// Хеширует строку.
    /// </summary>
    /// <param name="stringToEncrypt">Строка, которую нужно хешировать.</param>
    /// <returns>Хешированная строка.</returns>
    public static string GetBase64Hash(string stringToEncrypt)
    {
        var buffer = Encoding.UTF8.GetBytes(stringToEncrypt);
        HashAlgorithm sha = SHA256.Create();
        byte[] hash = sha.ComputeHash(buffer);

        return Convert.ToBase64String(hash);
    }
}