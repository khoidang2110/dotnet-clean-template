using MediatR;
using domain.Entities;

namespace application.Features.Products.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }
}
