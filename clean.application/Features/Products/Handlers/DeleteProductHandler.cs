using MediatR;
using application.Features.Products.Commands;
using application.Contract.Repo;

namespace application.Features.Products.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _repo;

    public DeleteProductHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _repo.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
