using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.IdentityServer;

[Route("identity")]
[Authorize]
public class IdentityController : ControllerBase
{
    private readonly TokenService _tokenService;

    public IdentityController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
    }

    [HttpGet("call-api")]
    public async Task<IActionResult> CallApi()
    {
        try
        {
            var token = await _tokenService.GetApiTokenAsync();
            var apiResult = await _tokenService.CallApiAsync(token);
            return Ok(apiResult);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
  
}