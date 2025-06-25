using MediatR;

namespace application.Features.Products.Commands;

public class UpdateProductCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Price { get; set; }
}
