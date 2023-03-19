using BlackLink_Commends.Commend.InterestCommends.Commend;
using BlackLink_Services.InterestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestController : Controller
    {
        private readonly IInterestService service;
        public InterestController(IInterestService service)
        {
            this.service = service;
        }
        #region Actions
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddInterests(AddInterestCommend commend)
        {
            var interest = await service.AddInterest(commend);
            return Ok(interest);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateInterest(UpdateInterestCommend commend)
        {
            var interest = await service.UpdateInterest(commend);
            return Ok(interest);
        }


        #endregion

        #region Get
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAllInterests()
        {
            var interest = await service.GetAllInterests();
            return Ok(interest);
        }

        #endregion

        #region Remove
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> RemoveInterests(Guid Id)
        {
            await service.RemoveInterest(Id);
            return Ok();
        }

        #endregion
    }
}
