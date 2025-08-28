using Microsoft.AspNetCore.Http;

namespace Business.Shared
{
    public interface IFileStorageService
    {
        Task<string> SaveFile(string containerName, IFormFile file);
        Task<string> EditFile(string containerName, IFormFile file, string fileRoute);
        Task DeleteFile(string fileRoute, string containerName);
    }
}
