using LedgerLink.Domain.Enums;

namespace LedgerLink.Application.Features.Requests.Dtos;

public class ServiceRequestDto
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = string.Empty; // Join from service table
    public decimal Price { get; set; }
    public string Status { get; set; } = string.Empty; // Comes from enum of status
    public string ClientName { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

}