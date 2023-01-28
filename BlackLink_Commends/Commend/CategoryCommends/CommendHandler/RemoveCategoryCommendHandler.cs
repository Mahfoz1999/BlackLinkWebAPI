using BlackLink_Commends.Commend.CategoryCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.CategoryCommends.CommendHandler;

public class RemoveCategoryCommendHandler : IRequestHandler<RemoveCategoryCommend, Category>
{
    private readonly BlackLinkDbContext Context;
    public RemoveCategoryCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Category> Handle(RemoveCategoryCommend request, CancellationToken cancellationToken)
    {
        try
        {
            Category? category = await Context.Categories.FindAsync(request.Id);
            if (category == null) throw new NotFoundException("Category Not Found");
            Context.Categories.Remove(category);
            await Context.SaveChangesAsync(cancellationToken);
            return category;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
