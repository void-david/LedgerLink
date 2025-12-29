namespace LedgerLink.Application.Common.Interfaces;

public interface IFileService
{
    // Save file and return its path to store in DB
    Task<string> SaveFileAsync(Stream fileStream, string fileName);

    // Get the file for downloading it
    Task<Stream> GetFileAsync(string filePath);
}