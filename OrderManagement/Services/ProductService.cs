using OrderManagement.DTOs;
using OrderManagement.Models;
using OrderManagement.Repositories;

namespace OrderManagement.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse?> GetByIdAsync(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        return ToResponse(product);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            StockQuantity = request.StockQuantity
        };

        var savedProduct = await _productRepository.AddAsync(product);

        return ToResponse(savedProduct);
    }

    public async Task<List<ProductResponse>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products
            .Select(ToResponse)
            .ToList();
    }

    public async Task<ProductResponse?> UpdateAsync(long id, UpdateProductRequest request)
    {
        var updatedProduct = new Product
        {
            Name = request.Name,
            Price = request.Price,
            StockQuantity = request.StockQuantity
        };

        var product = await _productRepository.UpdateAsync(id, updatedProduct);

        if (product is null)
        {
            return null;
        }

        return ToResponse(product);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _productRepository.DeleteAsync(id);
    }

    private static ProductResponse ToResponse(Product product)
    {
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Price,
            product.StockQuantity
        );
    }
}