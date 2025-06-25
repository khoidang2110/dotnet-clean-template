using MediatR;

namespace application.Features.Products.Commands;

public class DeleteProductCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
