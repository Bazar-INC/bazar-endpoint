﻿
using Shared.FileStorage.Abstract;
using static Shared.AppSettings;

namespace Shared.FileStorage;

public class FileStorageService : IFileStorageService
{
    #region Public
    public void DeleteFile(string path)
    {
        if (IsFileExist(path))
        {
            File.Delete(path);
        }

        var fullPath = Path.Combine(generateCdnPath(), path);
        if (IsFileExist(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    public string SaveUserAvatar(string base64file, string oldFileName, Guid itemId)
    {
        return saveItemWithCdnSubfolder(base64file, oldFileName, itemId, CdnPaths.UsersAvatars.Item1);
    }

    public string SaveCategoryIcon(string base64file, string oldFileName, Guid itemId)
    {
        return saveItemWithCdnSubfolder(base64file, oldFileName, itemId, CdnPaths.CategoryIcons.Item1);
    }

    public string SaveCategoryImage(string base64file, string oldFileName, Guid itemId)
    {
        return saveItemWithCdnSubfolder(base64file, oldFileName, itemId, CdnPaths.CategoryImages.Item1);
    }

    public string SaveFile(string base64file, string oldFileName, Guid itemId)
    {
        return saveItemWithCdnSubfolder(base64file, oldFileName, itemId);
    }

    public string SaveProductImage(string base64file, string oldFileName)
    {
        return saveItemWithCdnSubfolder(base64file, oldFileName, Guid.NewGuid(), CdnPaths.ProductImages.Item1);
    }
    #endregion

    #region Private
    private string generateCdnPath()
    {
        string workingDirectory = Environment.CurrentDirectory;

        string cdnDirectory = CdnPaths.CdnDirectory.Item1;

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

    public string SaveFile(string base64file, string fileName)
    {
        var cdnPath = generateCdnPath();

        var itemsPath = cdnPath;

        createDirectoryIfNotExists(itemsPath);

        var filePath = Path.Combine(itemsPath, fileName);

        File.WriteAllBytes(filePath, Convert.FromBase64String(base64file));

        return filePath;
    }

    public bool IsFileExist(string path)
    {
        return File.Exists(path) || 
               File.Exists(Path.Combine(generateCdnPath(), path));
    }
    #endregion
}
