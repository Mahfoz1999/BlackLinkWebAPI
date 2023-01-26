using BlackLink_Commends.CategoryCommends.Query;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.CategoryCommends.QueryHandler;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
{
	private readonly BlackLinkDbContext Context;
	public GetCategoryByIdQueryHandler(BlackLinkDbContext Context)
	{
		this.Context = Context;
	}
	public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken) =>
		await Context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)!;

}
