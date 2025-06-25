using domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using application.Contract.Repo;

namespace application.Services;

public class ProductService
{
    private readonly IProductRepository _repo;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repo, ILogger<ProductService> logger)
    {
        _repo = repo;
                _logger = logger;

    }


    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all products...");
        var products = await _repo.GetAllAsync();
        _logger.LogInformation("Found {Count} products", products.Count());
        return products;
    }
    public Task<Product?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task AddAsync(Product product) => _repo.AddAsync(product);

    public Task UpdateAsync(Product product) => _repo.UpdateAsync(product);

    public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
}
