using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.CategoryCommends.Commend;
public record AddCategoryCommend(string Name) : IRequest<Category>;
public record UpdateCategoryCommend(Guid Id, string Name) : IRequest<Category>;
public record RemoveCategoryCommend(Guid Id) : IRequest<Category>;