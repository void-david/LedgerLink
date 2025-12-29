using LedgerLink.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LedgerLink.Application.Features.Documents.Queries.GetDocument;

public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, FileDownloadDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileService _fileService;

    public GetDocumentQueryHandler(IApplicationDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<FileDownloadDto> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
    {
        // 1. Find metadata in DB
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.DocumentId, cancellationToken);

        if (document == null) throw new Exception("Document not found in database.");

        // 2. Get physical stream from storage (Local or azure)
        var stream = await _fileService.GetFileAsync(document.FilePath);

        // 3. Return combined result
        return new FileDownloadDto
        {
            FileStream = stream,
            ContentType = "application/octet-stream",
            FileName = document.FileName
        };
    }
}