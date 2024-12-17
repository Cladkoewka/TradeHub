using CatalogService.Application.DTOs;
using CatalogService.Domain.Entities;
using Mapster;

namespace CatalogService.Application.Mapping;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Product, ProductGetDto>.NewConfig();
        TypeAdapterConfig<ProductCreateDto, Product>.NewConfig();
        TypeAdapterConfig<ProductUpdateDto, Product>.NewConfig()
            .IgnoreNullValues(true);
    }
}