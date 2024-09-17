namespace AdvertBoard.Hosts.Api.Helpers;

public static class FormFileHelper
{
    public static byte[] RequestFileToImage(IFormFile file)
    {
        using var fileStream = file.OpenReadStream();
        byte[] bytes = new byte[file.Length];
        fileStream.Read(bytes, 0, (int)file.Length);
        return bytes;
    }
}