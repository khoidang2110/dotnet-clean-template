using MediatR;
using domain.Entities;
using application.Features.Products.Commands;
using application.Contract.Repo;

namespace application.Features.Products.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _repo;

    public CreateProductHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product { Name = request.Name, Price = request.Price };
        await _repo.AddAsync(product);
        return product;
    }
}
