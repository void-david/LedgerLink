using LedgerLink.Domain.Common;

namespace LedgerLink.Domain.Entities;

public class Document : BaseEntity
{
    public int ServiceRequestId { get; set; }
    public ServiceRequest? ServiceRequest { get; set; }

    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty; // Path in storage
    public long FileSizeBytes { get; set; }

}