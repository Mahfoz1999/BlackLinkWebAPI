using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommends.Query;

public record GetAllBlogsQuery() : IRequest<IEnumerable<Blog>>;
public record GetBlogByIdQuery(Guid Id) : IRequest<Blog>;

