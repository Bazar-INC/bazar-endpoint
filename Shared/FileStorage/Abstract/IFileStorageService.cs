
namespace Shared.FileStorage.Abstract;

public interface IFileStorageService
{
    string SaveProductImage(string base64file, string oldFileName);
    string SaveCategoryImage(string base64file, string oldFileName, Guid itemId);
    string SaveCategoryIcon(string base64file, string oldFileName, Guid itemId);
    string SaveUserAvatar(string base64file, string oldFileName, Guid itemId);
    string SaveFile(string base64file, string oldFileName, Guid itemId);
    string SaveFile(string base64file, string fileName);
    void DeleteFile(string path);
    bool IsFileExist(string path);
}
