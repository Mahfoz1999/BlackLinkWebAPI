using BlackLink_Commends.Commend.CategoryCommends.Commend;
using BlackLink_DTO.Category;

namespace BlackLink_Services.CategoryService;

public interface ICategoryService
{
    public Task<CategoryDto> GetCategoryById(Guid id);
    public Task<IEnumerable<CategoryDto>> GetAllCategories();
    public Task<AddCategoryCommend> AddCategory(AddCategoryCommend addCategoryCommned);
    public Task<UpdateCategoryCommend> UpdateCategory(UpdateCategoryCommend updateCategoryCommend);
    public Task RemoveCategory(Guid Id);

}
