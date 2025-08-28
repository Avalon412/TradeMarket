using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Business.Shared.CloudServices
{
    public class AzureStorageService : IFileStorageService
    {
        private readonly string _connectionString;

        public AzureStorageService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage")
                ?? throw new InvalidOperationException("Connection string is not configured");
        }

        public async Task<string> SaveFile(string containerName, IFormFile file)
        {
            BlobClient? blob = null;

            try
            {
                var client = new BlobContainerClient(_connectionString, containerName);
                await client.CreateIfNotExistsAsync();
                client.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                var extention = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid()}{extention}";
                blob = client.GetBlobClient(fileName);
                await blob.UploadAsync(file.OpenReadStream());
            }
            catch (RequestFailedException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return blob?.Uri.ToString() ?? "";
        }

        public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
        {
            await DeleteFile(fileRoute, containerName);
            return await SaveFile(containerName, file);
        }

        public async Task DeleteFile(string fileRoute, string containerName)
        {
            if (string.IsNullOrEmpty(fileRoute))
            {
                return;
            }

            var client = new BlobContainerClient(_connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            var fileName = Path.GetFileName(fileRoute);
            var blob = client.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();
        }
    }
}
