using System.Reflection.Metadata;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LedgerLink.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LedgerLink.Infrastructure.Services;

public class AzureBlobFileService : IFileService
{
    private readonly BlobContainerClient _containerClient;

    public AzureBlobFileService(IConfiguration configuration)
    {   
        var connectionString = configuration.GetConnectionString("AzureStorage");
        var blobServiceClient = new BlobServiceClient(connectionString);

        _containerClient = blobServiceClient.GetBlobContainerClient("documents");
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
    {
        // 1. Generate unique name
        var uniqueName = $"{Guid.NewGuid()}_{fileName}";

        // 2. Get reference to the blob 
        var blobClient = _containerClient.GetBlobClient(uniqueName);

        // 3. Upload
        fileStream.Position = 0;
        await blobClient.UploadAsync(fileStream, new BlobUploadOptions
        {
           HttpHeaders = new BlobHttpHeaders { ContentType = "application/octet-stream" } 
        });

        return uniqueName;    
    }

    public async Task<Stream> GetFileAsync(string filePath)
    {
        var blobClient = _containerClient.GetBlobClient(filePath);

        if (!await blobClient.ExistsAsync())
        {
            throw new FileNotFoundException($"Blob not found: {filePath}");
        }

        var downloadResult = await blobClient.DownloadAsync();
        return downloadResult.Value.Content;
    }
}