using BlackLink_DTO.Category;

namespace BlackLink_Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<CategoryFormDto> AddCategory(CategoryFormDto formDto);
        public Task<CategoryFormDto> UpdateCategory(CategoryFormDto formDto);
        public Task<IEnumerable<CategoryDto>> GetAllCategories();
        public Task<CategoryDto> GetCategory(Guid Id);
        public Task RemoveCategory(Guid Id);

    }
}
