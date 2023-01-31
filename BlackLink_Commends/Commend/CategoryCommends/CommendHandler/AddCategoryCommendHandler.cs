using BlackLink_Commends.Commend.CategoryCommends.Commend;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.CategoryCommends.CommendHandler;

public class AddCategoryCommendHandler : IRequestHandler<AddCategoryCommend, Category>
{
    private readonly BlackLinkDbContext Context;
    public AddCategoryCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Category> Handle(AddCategoryCommend request, CancellationToken cancellationToken)
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
