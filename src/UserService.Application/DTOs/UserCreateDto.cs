namespace UserService.Application.DTOs;

public record UserCreateDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);