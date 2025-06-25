using MediatR;
using domain.Entities;

namespace application.Features.Products.Commands;

public class CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; } = default!;
    public int Price { get; set; }
}
