using BlackLink_Commends.Commend.CategoryCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.CategoryCommends.CommendHandler;

public class UpdateCategoryCommendHandler : IRequestHandler<UpdateCategoryCommend, Category>
{
    private readonly BlackLinkDbContext Context;
    public UpdateCategoryCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }
    public async Task<Category> Handle(UpdateCategoryCommend request, CancellationToken cancellationToken)
    {
        Category? category = await Context.Categories.FindAsync(request.Id);
        if (category is null) throw new NotFoundException("Category Not Found");
        category.Name = request.Name;
        Context.Categories.Update(category);
        await Context.SaveChangesAsync();
        return category;

    }
}
