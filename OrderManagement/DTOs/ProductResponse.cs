namespace OrderManagement.DTOs;

public record ProductResponse(
    long ProductId,
    string Name,
    decimal Price,
    int StockQuantity
);