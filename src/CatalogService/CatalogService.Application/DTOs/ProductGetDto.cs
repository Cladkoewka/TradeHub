namespace CatalogService.Application.DTOs;

public record ProductGetDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity
);