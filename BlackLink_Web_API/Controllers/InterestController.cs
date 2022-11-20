using BlackLink_DTO.Interest;
using BlackLink_Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestController : Controller
    {
        private readonly IInterestRepository repository;
        public InterestController(IInterestRepository repository)
        {
            this.repository = repository;
        }
        #region Actions
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> AddInterests(InterestFormDto formDto)
        {
            var interest = await repository.AddInterests(formDto);
            return Ok(interest);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UpdateInterests(InterestFormDto formDto)
        {
            var interest = await repository.UpdateInterests(formDto);
            return Ok(interest);
        }
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> AddUserInterests(List<Guid> Ids)
        {
            var interest = await repository.AddUserInterests(Ids);
            return Ok(interest);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUserInterests(List<Guid> Ids)
        {
            var interest = await repository.UpdateUserInterests(Ids);
            return Ok(interest);
        }
        #endregion

        #region Get
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAllInterests()
        {
            var interest = await repository.GetAllInterests();
            return Ok(interest);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetUserInterests()
        {
            var interest = await repository.GetUserInterests();
            return Ok(interest);
        }
        #endregion

        #region Remove
        [HttpDelete]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> RemoveInterests(Guid userId)
        {
            var interest = await repository.RemoveInterests(userId);
            return Ok(interest);
        }
        [HttpDelete]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> RemoveUserInterests(List<Guid> Ids)
        {
            var interest = await repository.RemoveUserInterests(Ids);
            return Ok(interest);
        }
        #endregion
    }
}
