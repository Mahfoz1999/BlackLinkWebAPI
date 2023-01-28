using BlackLink_Commends.Commend.CategoryCommends.Query;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.CategoryCommends.QueryHandler;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    private readonly BlackLinkDbContext Context;
    public GetAllCategoriesQueryHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await Context.Categories.ToListAsync(cancellationToken);
    }
}
