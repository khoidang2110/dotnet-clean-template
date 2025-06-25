using MediatR;
using domain.Entities;
using System.Collections.Generic;

namespace application.Features.Products.Queries;

public class GetAllProductsQuery : IRequest<List<Product>>
{
}
