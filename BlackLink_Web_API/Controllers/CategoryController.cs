using BlackLink_Commends.Commend.CategoryCommends.Commend;
using BlackLink_DTO.Category;
using BlackLink_Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService service;
        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddCategory(AddCategoryCommned addCategoryCommned)
        {
            AddCategoryCommned category = await service.AddCategory(addCategoryCommned);
            return Ok(category);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommend updateCategoryCommend)
        {
            UpdateCategoryCommend category = await service.UpdateCategory(updateCategoryCommend);
            return Ok(category);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAllCategories()
        {
            IEnumerable<CategoryDto> category = await service.GetAllCategories();
            return Ok(category);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetCategory(Guid Id)
        {
            CategoryDto category = await service.GetCategoryById(Id);
            return Ok(category);
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> RemoveCategory(RemoveCategoryCommend removeCategoryCommend)
        {
            RemoveCategoryCommend result = await service.RemoveCategory(removeCategoryCommend);
            return Ok(result);
        }
    }
}
