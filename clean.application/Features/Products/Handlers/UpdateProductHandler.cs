using MediatR;
using application.Features.Products.Commands;
using application.Contract.Repo;

namespace application.Features.Products.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _repo;

    public UpdateProductHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repo.GetByIdAsync(request.Id);
        if (product != null)
        {
            product.Name = request.Name;
            product.Price = request.Price;
            await _repo.UpdateAsync(product);
        }

        return Unit.Value;
    }
}
