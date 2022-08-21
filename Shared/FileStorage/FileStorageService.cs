
using Shared.FileStorage.Abstract;
using System.Reflection;

namespace Shared.FileStorage;

public class FileStorageService : IFileStorageService
{
    public void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public string SaveCategoryIcon(string base64file, string oldFileName, Guid itemId)
    {
        return saveItemWithCdnSubfolder(base64file, oldFileName, itemId, @"Categories\Icons\");
    }

    public string SaveCategoryImage(string base64file, string oldFileName, Guid itemId)
    {
        return saveItemWithCdnSubfolder(base64file, oldFileName, itemId, @"Categories\Images\");
    }

    public string SaveFile(string base64file, string oldFileName, Guid itemId)
    {
        return saveItemWithCdnSubfolder(base64file, oldFileName, itemId);
    }

    public string SaveProductImage(string base64file, string oldFileName, Guid itemId)
    {
        return saveItemWithCdnSubfolder(base64file, oldFileName, itemId, @"Products\Images\");
    }

    private string generateCdnPath()
    {
        string workingDirectory = Environment.CurrentDirectory;

        string cdnDirectory = @"Cdn\";

        string cdnPath = Path.Combine(workingDirectory, cdnDirectory);

        // This will create a cdn directory if it is not exist
        createDirectoryIfNotExists(cdnPath);

        return cdnPath;
    }

    private void createDirectoryIfNotExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private string generateFileName(string oldFileName, Guid itemId)
    {
        var fileExtension = Path.GetExtension(oldFileName);

        if (string.IsNullOrEmpty(fileExtension))
        {
            fileExtension = ".jpg";
        }

        // This will generate fileName [guid].ext
        var fileName = itemId.ToString() + fileExtension;

        return fileName;
    }

    private string saveItemWithCdnSubfolder(string base64file, string oldFileName, Guid itemId, string subfolderPath = "")
    {
        var fileName = generateFileName(oldFileName, itemId);

        var cdnPath = generateCdnPath();

        var itemsPath = cdnPath;

        if (!string.IsNullOrEmpty(subfolderPath))
        {
            itemsPath = Path.Combine(cdnPath, subfolderPath);
        }

        createDirectoryIfNotExists(itemsPath);

        var filePath = Path.Combine(itemsPath, fileName);

        File.WriteAllBytes(filePath, Convert.FromBase64String(base64file));

        return filePath;
    }
}
