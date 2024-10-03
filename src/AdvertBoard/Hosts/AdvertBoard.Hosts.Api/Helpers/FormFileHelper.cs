namespace AdvertBoard.Hosts.Api.Helpers;

/// <summary>
/// Конвертирует изображение.
/// </summary>
public static class FormFileHelper
{
    /// <summary>
    /// Конвертирует файл из <see cref="IFormFile"/> в <see cref="byte"/>.
    /// </summary>
    /// <param name="file">Файл изображения.</param>
    /// <returns>Содержимое изображения.</returns>
    public static byte[] RequestFileToImage(IFormFile file)
    {
        using var fileStream = file.OpenReadStream();
        byte[] bytes = new byte[file.Length];
        fileStream.Read(bytes, 0, (int)file.Length);
        return bytes;
    }
}