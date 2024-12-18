using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.DTOs;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.Services;

public class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly IRoleRepository _roleRepository;
    private readonly string _jwtKey;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;

    public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, PasswordHasher passwordHasher, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
            
        _jwtKey = configuration["Jwt:Key"];
        _jwtIssuer = configuration["Jwt:Issuer"];
        _jwtAudience = configuration["Jwt:Audience"];
    }

    public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequest)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequest.Email);

        if (user == null || !_passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = GenerateJwtToken(user);
        return new LoginResponseDto(token);
    }
    
    public async Task RegisterAsync(UserCreateDto userCreateDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(userCreateDto.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

        var role = await _roleRepository.GetByNameAsync("User")
                   ?? throw new InvalidOperationException("Default role not found.");

        var user = userCreateDto.Adapt<User>();
    
        user.Id = Guid.NewGuid();
        user.PasswordHash = _passwordHasher.HashPassword(userCreateDto.Password);
        user.RoleId = role.Id;

        await _userRepository.CreateAsync(user);
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_jwtKey);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _jwtIssuer,
            _jwtAudience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
