namespace OrderManagement.DTOs;

public record UpdateProductRequest(
    string Name,
    decimal Price,
    int StockQuantity
);