using BlackLink_Models.Models;
using MediatR;
namespace BlackLink_Commends.CategoryCommends.Query;

public class GetCategoryByIdQuery : IRequest<Category>
{
    public Guid Id { get; set; }
}
