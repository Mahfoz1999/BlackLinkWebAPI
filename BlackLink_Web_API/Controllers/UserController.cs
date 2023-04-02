using BlackLink_Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_Web_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService service;
    public UserController(IUserService service)
    {
        this.service = service;
    }
    [HttpGet]
    [Route("[action]")]
    [Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await service.GetAllUsers();
        return Ok(result);
    }
}
