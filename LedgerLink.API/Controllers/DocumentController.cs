using LedgerLink.Application.Features.Documents.Commands.UploadDocument;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedgerLink.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Upload([FromForm] int serviceRequestId, [FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file provided.");
        }

        using var stream = file.OpenReadStream();

        var command = new UploadDocumentCommand(
            serviceRequestId,
            file.FileName,
            stream,
            file.Length
        );
        
        var documentId = await _mediator.Send(command);
        return Ok(documentId);
    }
}