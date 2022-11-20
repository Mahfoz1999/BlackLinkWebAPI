using BlackLink_DTO.Groub;
using BlackLink_Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroubController : Controller
    {
        private readonly IGroubRepository repository;
        public GroubController(IGroubRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> CreateGroub(GroubFormDto formDto)
        {
            var groub = await repository.CreateGroub(formDto);
            return Ok(groub);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UpdateGroub(GroubFormDto formDto)
        {
            var groub = await repository.UpdateGroub(formDto);
            return Ok(groub);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> JoinToGroub(Guid groubId)
        {
            await repository.JoinToGroub(groubId);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> LeaveGroub(Guid groubId)
        {
            await repository.LeaveGroub(groubId);
            return Ok();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAllGroubs()
        {
            var groubs = await repository.GetAllGroubs();
            return Ok(groubs);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetGroub(Guid Id)
        {
            var groubs = await repository.GetGroub(Id);
            return Ok(groubs);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetUserGroubs(Guid Id)
        {
            var groubs = await repository.GetUserGroubs(Id);
            return Ok(groubs);
        }
        [HttpDelete]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> RemoveGroub(Guid Id)
        {
            var groubs = await repository.RemoveGroub(Id);
            return Ok(groubs);
        }
    }
}
