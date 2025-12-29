using LedgerLink.Application.Features.Documents.Commands.UploadDocument;
using LedgerLink.Application.Features.Documents.Queries.GetDocument;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> Download(int id)
    {
        try
        {
            var result = await _mediator.Send(new GetDocumentQuery(id));
            // This return type handles the headers for download automatically
            return File(result.FileStream, result.ContentType, result.FileName);
        }
        catch (FileNotFoundException)
        {
            return NotFound("File does not exist on server.");
        }   
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    public async Task<ActionResult<int>> Upload([FromForm] UploadDocumentRequest request)
    {
        if (request.File == null || request.File.Length == 0)
        {
            return BadRequest("No file provided.");
        }

        using var stream = request.File.OpenReadStream();

        var command = new UploadDocumentCommand(
            request.serviceRequestId,
            request.File.FileName,
            stream,
            request.File.Length
        );
        
        var documentId = await _mediator.Send(command);
        return Ok(documentId);
    }
}

public class UploadDocumentRequest
{
    public int serviceRequestId { get; set; }
    public IFormFile File { get; set; }
}