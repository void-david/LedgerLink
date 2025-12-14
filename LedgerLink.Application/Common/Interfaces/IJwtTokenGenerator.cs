using LedgerLink.Domain.Entities;

namespace LedgerLink.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}