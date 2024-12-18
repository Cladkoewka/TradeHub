namespace UserService.Application.DTOs;

public record UserUpdateDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);