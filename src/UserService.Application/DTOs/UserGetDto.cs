namespace UserService.Application.DTOs;

public record UserGetDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);