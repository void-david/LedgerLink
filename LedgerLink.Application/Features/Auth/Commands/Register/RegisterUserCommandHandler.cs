using LedgerLink.Application.Common.Interfaces;
using LedgerLink.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LedgerLink.Application.Features.Auth.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
{
    private readonly IApplicationDbContext _context;

    public RegisterUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Check if email exists
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            throw new Exception("Email already in use.");
        }

        // 2. Create the User Login
        var user = new User
        {
            Email = request.Email,
            PasswordHash= request.Password, // In real app: BCrypt.HashPassword(request.Password)
            Role = "Client" // Default role: Client
        };

        // 3. Create the Linked Client Profile
        // Linking Login (User) to Business Data (Client)
        var clientProfile = new Client
        {
            User = user,
            Name = request.FullName,
            Rfc = "XAXX010101000", // Placeholder until they update profile
            ContactEmail = request.Email,
            PhoneNumber = ""  
        };

        _context.Clients.Add(clientProfile);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}