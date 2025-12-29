namespace LedgerLink.Application.Features.Documents.Queries.GetDocument;

public class FileDownloadDto
{
    public Stream FileStream { get; set; } = null!;
    public string ContentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}