using CatalogService.Application.DTOs;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;
using Mapster;

namespace CatalogService.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductGetDto>> GetAllProductsAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Adapt<IEnumerable<ProductGetDto>>();
    }

    public async Task<ProductGetDto?> GetProductByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        return product?.Adapt<ProductGetDto>();
    }

    public async Task<ProductGetDto?> AddProductAsync(ProductCreateDto dto)
    {
        var product = dto.Adapt<Product>();
        product.Id = Guid.NewGuid(); // Назначаем новый ID
        await _repository.AddAsync(product);
        return product.Adapt<ProductGetDto>();
    }

    public async Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto dto)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) 
            return false;

        dto.Adapt(product); 
        await _repository.UpdateAsync(product);
        return true;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) 
            return false;

        await _repository.DeleteAsync(product);
        return true;
    }
}