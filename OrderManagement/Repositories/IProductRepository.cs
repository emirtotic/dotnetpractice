using OrderManagement.Models;

namespace OrderManagement.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(long id);
    Task<Product> AddAsync(Product product);

    // Task<T> je ekvivalent Future<T> u Javi, odnosno CompletableFuture<T> u Javi 8 i novijim verzijama. To je asinhroni tip koji predstavlja rezultat operacije koja ce biti dostupna u buducnosti. U C# se koristi async/await sintaksa za rad sa asinhronim operacijama, dok se u Javi koristi thenApply, thenAccept, thenCompose i slicne metode za obradu rezultata asinhronih operacija
    // Product?
}