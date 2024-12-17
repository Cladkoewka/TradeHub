namespace CatalogService.Application.DTOs;

public record ProductCreateDto(
    string Name,
    string Description,
    decimal Price,
    int Quantity
);