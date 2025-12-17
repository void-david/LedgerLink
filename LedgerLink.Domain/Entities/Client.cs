using LedgerLink.Domain.Common;

namespace LedgerLink.Domain.Entities;

public class Client : BaseEntity
{
    public int? UserId { get; set; } // Foreign key, now nullable
    public User? User { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Rfc { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty; // Usually different from login email
    public string PhoneNumber { get; set; } = string.Empty;

    public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>(); 
}