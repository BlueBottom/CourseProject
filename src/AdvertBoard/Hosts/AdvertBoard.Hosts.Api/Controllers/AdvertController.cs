using AdvertBoard.Application.AppServices.Contexts.Adverts.Services;
using AdvertBoard.Contracts.Contexts.Adverts;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdvertController : ControllerBase
{
    private readonly IAdvertService _advertService;

    public AdvertController(IAdvertService advertService)
    {
        _advertService = advertService; 
    }
    
    [HttpPost("search")]
    public async Task<IActionResult> GetAllAsync(GetAllAdvertsDto dto, CancellationToken cancellationToken)
    {
        var result = await _advertService.GetAllAsync(dto, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] CreateAdvertDto dto, CancellationToken cancellationToken)
    {
        var result = await _advertService.AddAsync(dto, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm]UpdateAdvertDto dto, CancellationToken cancellationToken)
    {
        var result = await _advertService.UpdateAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}