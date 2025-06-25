using MediatR;
using domain.Entities;
using application.Features.Products.Queries;
using application.Contract.Repo;

namespace application.Features.Products.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
{
    private readonly IProductRepository _repo;

    public GetAllProductsHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync();
    }
}
