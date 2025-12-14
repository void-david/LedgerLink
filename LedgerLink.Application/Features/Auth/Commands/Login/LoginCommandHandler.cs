using System.ComponentModel.DataAnnotations;
using LedgerLink.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

// TODO: Hash passwords

namespace LedgerLink.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenGenerator _jwtGenerator;

    public LoginCommandHandler(IApplicationDbContext context, IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _jwtGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // 1. Find User
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        
        // 2. Validate password 
        // TODO: Hash passwords
        if (user == null || user.PasswordHash != request.Password)
        {   
            throw new Exception("Invalid Credentials");
        }

        // 3. Generate Token
        var token = _jwtGenerator.GenerateToken(user);

        return new LoginResult(token, user.Role, user.Email);
    }
}