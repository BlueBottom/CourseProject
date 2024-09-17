using AdvertBoard.Application.AppServices.Contexts.Images.Services;
using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Hosts.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _imageService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpPost("/Advert/{advertId:guid}/images/")]
    public async Task<IActionResult> AddAsync(Guid advertId, IFormFile file, CancellationToken cancellationToken)
    {
        var image = FormFileHelper.RequestFileToImage(file);
        var dto = new CreateImageDto
        {
            AdvertId = advertId,
            File = image
        };
            var result = await _imageService.AddAsync(dto, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _imageService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}