using LedgerLink.Domain.Common;
using LedgerLink.Domain.Enums;

namespace LedgerLink.Domain.Entities;

public class ServiceRequest : BaseEntity
{
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public int ServiceId { get; set; }
    public Service? Service { get; set; }

    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    public string Notes { get; set; } = string.Empty;
    
    // One request can have many documents
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}