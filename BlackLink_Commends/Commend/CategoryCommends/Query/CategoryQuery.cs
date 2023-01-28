using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.CategoryCommends.Query;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Category>;
public record GetAllCategoriesQuery() : IRequest<IEnumerable<Category>>;
