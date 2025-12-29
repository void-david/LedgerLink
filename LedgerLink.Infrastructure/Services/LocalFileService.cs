using LedgerLink.Application.Common.Interfaces;
using Microsoft.Identity.Client;

namespace LedgerLink.Infrastructure.Services;

public class LocalFileService : IFileService
{
    private readonly string _basePath;

    public LocalFileService()
    {
        _basePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
    {
        // 1. Generate a unique safe name id: "guid_fileName.pdf"
        var uniqueName = $"{Guid.NewGuid()}_{fileName}";

        // 2. Combine with base path
        var fullPath = Path.Combine(_basePath, uniqueName);

        // 3. Write to disk
        using (var output = new FileStream(fullPath, FileMode.Create))
        {
            await fileStream.CopyToAsync(output);
        }
        
        return uniqueName;
    }

    public Task<Stream> GetFileAsync(string filePath)
    {
        var fullPath = Path.Combine(_basePath, filePath);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"File not found: {fullPath}");
        }
        return Task.FromResult<Stream>(new FileStream(fullPath, FileMode.Open, FileAccess.Read));
    }

}