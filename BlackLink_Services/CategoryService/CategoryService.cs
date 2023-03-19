using BlackLink_Commends.Commend.CategoryCommends.Commend;
using BlackLink_Commends.Commend.CategoryCommends.Query;
using BlackLink_DTO.Category;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly IMediator _mediator;
    public CategoryService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<AddCategoryCommend> AddCategory(AddCategoryCommend addCategoryCommned)
    {
        await _mediator.Send(addCategoryCommned);
        return addCategoryCommned;
    }
    public async Task<UpdateCategoryCommend> UpdateCategory(UpdateCategoryCommend updateCategoryCommend)
    {
        await _mediator.Send(updateCategoryCommend);
        return updateCategoryCommend;
    }
    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        GetAllCategoriesQuery query = new();
        var response = await _mediator.Send(query);
        IEnumerable<CategoryDto> categories = response.Select(e => new CategoryDto()
        {
            Id = e.Id,
            Name = e.Name,
        }).ToList();
        return categories;
    }

    public async Task<CategoryDto> GetCategoryById(Guid id)
    {
        Category response = await _mediator.Send(new GetCategoryByIdQuery(id));
        CategoryDto category = new()
        {
            Id = response.Id,
            Name = response.Name,
        };
        return category;
    }
    public async Task RemoveCategory(Guid Id)
    {
        await _mediator.Send(new RemoveCategoryCommend(Id));
    }
}
