using OrderManagement.DTOs;

namespace OrderManagement.Services;

public interface IProductService
{
    Task<List<ProductResponse>> GetAllAsync();
    Task<ProductResponse?> GetByIdAsync(long id);
    Task<ProductResponse> CreateAsync(CreateProductRequest request);
    Task<ProductResponse?> UpdateAsync(long id, UpdateProductRequest request);
    Task<bool> DeleteAsync(long id);
}