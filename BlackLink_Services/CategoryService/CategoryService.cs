using BlackLink_Commends.CategoryCommends.Query;
using BlackLink_DTO.Category;
using MediatR;

namespace BlackLink_Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly IMediator _mediator;
    public CategoryService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CategoryDto> GetCategoryById(Guid id)
    {
        var commend = new GetCategoryByIdQuery() { Id = id };
        var response = await _mediator.Send(commend);
        CategoryDto category = new()
        {
            Id = response.Id,
            Name = response.Name,
        };
        return category;
    }
}
