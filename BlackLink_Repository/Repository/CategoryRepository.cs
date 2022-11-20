using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Category;
using BlackLink_Models.Models;
using BlackLink_Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlackLinkDbContext Context;
        public CategoryRepository(BlackLinkDbContext Context)
        {
            this.Context = Context;
        }
        public async Task<CategoryFormDto> AddCategory(CategoryFormDto formDto)
        {
            Category newCategory = new()
            {
                Name = formDto.Name,
            };
            await Context.Categories.AddAsync(newCategory);
            await Context.SaveChangesAsync();
            formDto.Id = newCategory.Id;
            return formDto;
        }
        public async Task<CategoryFormDto> UpdateCategory(CategoryFormDto formDto)
        {
            int category = await Context.Categories.Where(category => category.Id == formDto.Id)
                .ExecuteUpdateAsync(c => c.SetProperty(a => a.Name, formDto.Name));
            if (category is not 0)
            {
                await Context.SaveChangesAsync();
                return formDto;
            }
            else throw new KeyNotFoundException("Category Not Found");
        }
        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            IEnumerable<CategoryDto> categories = await Context.Categories.Select(category => new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
            }).ToListAsync();
            return categories;
        }

        public async Task<CategoryDto> GetCategory(Guid Id)
        {
            CategoryDto? category = await Context.Categories.Where(category => category.Id == Id).Select(category => new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
            }).SingleOrDefaultAsync();
            return category is not null ? category : throw new KeyNotFoundException("Category Not Found");
        }

        public async Task RemoveCategory(Guid Id)
        {
            int category = await Context.Categories.Where(category => category.Id == Id).ExecuteDeleteAsync();
            if (category is not 0)
            {
                await Context.SaveChangesAsync();
            }
            else throw new KeyNotFoundException("Category Not Found");
        }


    }
}
