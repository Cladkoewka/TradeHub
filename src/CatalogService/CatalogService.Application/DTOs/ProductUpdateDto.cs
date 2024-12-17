namespace CatalogService.Application.DTOs;

public record ProductUpdateDto(
    string Name,
    string Description,
    decimal Price,
    int Quantity
);