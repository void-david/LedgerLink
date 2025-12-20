using LedgerLink.Domain.Enums;

namespace LedgerLink.Application.Features.Requests.Queries.GetMyRequests;

public class ServiceRequestDto
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = string.Empty; // Join from service table
    public decimal Price { get; set; }
    public string Status { get; set; } = string.Empty; // Comes from enum of status
    public DateTime CreatedAt { get; set; }

}