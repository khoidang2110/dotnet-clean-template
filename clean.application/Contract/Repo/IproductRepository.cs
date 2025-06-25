using domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace application.Contract.Repo;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(); // Thêm dòng này
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
