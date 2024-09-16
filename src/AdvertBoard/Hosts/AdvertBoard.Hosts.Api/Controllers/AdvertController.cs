using AdvertBoard.Application.AppServices.Contexts.Adverts.Services;
using AdvertBoard.Contracts.Contexts.Adverts;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdvertController : ControllerBase
{
    private readonly IAdvertService _service;

    public AdvertController(IAdvertService service)
    {
        _service = service;
    }
    
    [HttpPost("search")]
    public async Task<IActionResult> GetAllAsync(GetAllAdvertsDto dto, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(dto, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] CreateAdvertDto dto, CancellationToken cancellationToken)
    {
        var result = await _service.AddAsync(dto, cancellationToken);
        return Ok(result);
    }
}