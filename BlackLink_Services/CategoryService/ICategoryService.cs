using BlackLink_DTO.Category;

namespace BlackLink_Services.CategoryService;

public interface ICategoryService
{
    public Task<CategoryDto> GetCategoryById(Guid id);

}
