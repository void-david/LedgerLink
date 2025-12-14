using LedgerLink.Domain.Common;

namespace LedgerLink.Domain.Entities;

public class Client : BaseEntity
{
    public int UserId { get; set; } // Foreign key
    public User? User { get; set; }

    public string CompanyName { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>(); 
}