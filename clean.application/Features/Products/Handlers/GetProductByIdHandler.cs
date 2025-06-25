using MediatR;
using domain.Entities;
using application.Features.Products.Queries;
using application.Contract.Repo;

namespace application.Features.Products.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _repo;

    public GetProductByIdHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByIdAsync(request.Id);
    }
}
