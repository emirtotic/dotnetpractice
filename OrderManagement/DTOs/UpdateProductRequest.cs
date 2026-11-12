using System.ComponentModel.DataAnnotations;

namespace OrderManagement.DTOs;

public record UpdateProductRequest(

    [Required]
    [StringLength(100, MinimumLength = 2)]
    string Name,

    [Range(0.01, 999999999)]
    decimal Price,

    [Range(0, int.MaxValue)]
    int StockQuantity
);