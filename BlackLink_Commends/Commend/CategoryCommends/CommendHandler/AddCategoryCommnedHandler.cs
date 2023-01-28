using BlackLink_Commends.Commend.CategoryCommends.Commend;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.CategoryCommends.CommendHandler;

public class AddCategoryCommnedHandler : IRequestHandler<AddCategoryCommned, Category>
{
    private readonly BlackLinkDbContext Context;
    public AddCategoryCommnedHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Category> Handle(AddCategoryCommned request, CancellationToken cancellationToken)
    {
        Category category = new()
        {
            Name = request.Name,
        };
        await Context.Categories.AddAsync(category);
        await Context.SaveChangesAsync(cancellationToken);
        return category;
    }
}
