using BlackLink_DTO.Category;
using BlackLink_Repository.IRepository;
using BlackLink_Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ICategoryService service;
        public CategoryController(ICategoryRepository categoryRepository, ICategoryService service)
        {
            this.categoryRepository = categoryRepository;
            this.service = service;
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> AddCategory(CategoryFormDto formDto)
        {
            var category = await categoryRepository.AddCategory(formDto);
            return Ok(category);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UpdateCategory(CategoryFormDto formDto)
        {
            var category = await categoryRepository.UpdateCategory(formDto);
            return Ok(category);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAllCategories()
        {
            var category = await categoryRepository.GetAllCategories();
            return Ok(category);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetCategory(Guid Id)
        {
            var category = await service.GetCategoryById(Id);
            return Ok(category);
        }
        [HttpDelete]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> RemoveCategory(Guid Id)
        {
            await categoryRepository.RemoveCategory(Id);
            return Ok();
        }
    }
}
