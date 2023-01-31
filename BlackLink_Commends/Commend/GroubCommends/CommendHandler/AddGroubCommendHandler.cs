using BlackLink_Commends.Commend.GroubCommends.Commend;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.GroubCommends.CommendHandler;

public class AddGroubCommendHandler : IRequestHandler<AddGroubCommend, Groub>
{
    private readonly BlackLinkDbContext Context;
    public AddGroubCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Groub> Handle(AddGroubCommend request, CancellationToken cancellationToken)
    {
        try
        {
            Groub groub = new()
            {
                Admin = request.User,
                Name = request.Name,
                Description = request.Description,
            };
            await Context.Groubs.AddAsync(groub);
            await Context.SaveChangesAsync(cancellationToken);
            return groub;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
