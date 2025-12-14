using LedgerLink.Domain.Common;

namespace LedgerLink.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Client"; // "Admin" or "Client"

    // Navigation Property
    public Client? ClientProfile { get; set; }
}