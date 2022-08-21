
namespace Shared.FileStorage.Abstract;

public interface IFileStorageService
{
    string SaveProductImage(string base64file, string oldFileName, Guid itemId);
    string SaveCategoryImage(string base64file, string oldFileName, Guid itemId);
    string SaveCategoryIcon(string base64file, string oldFileName, Guid itemId);
    string SaveFile(string base64file, string path, Guid itemId);
    void DeleteFile(string path);
}
