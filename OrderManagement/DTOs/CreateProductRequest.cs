namespace OrderManagement.DTOs;

public record CreateProductRequest(
    string Name,
    decimal Price,
    int StockQuantity
);