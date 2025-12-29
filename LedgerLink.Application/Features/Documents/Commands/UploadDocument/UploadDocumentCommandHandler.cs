using LedgerLink.Application.Common.Interfaces;
using LedgerLink.Domain.Entities;
using MediatR;

namespace LedgerLink.Application.Features.Documents.Commands.UploadDocument;

public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileService _fileService;

    public UploadDocumentCommandHandler(IApplicationDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<int> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        // 1. Save physical file
        var storedPath = await _fileService.SaveFileAsync(request.FileContent, request.FileName);

        // 2. Create Entity (Document)
        var document = new Document
        {
            ServiceRequestId = request.ServiceRequestId,
            FileName = request.FileName,
            FilePath = storedPath, // Unique name we got from LocalFileService
            FileSizeBytes = request.FileSize
        };

        // 3. Save to DB
        _context.Documents.Add(document);
        await _context.SaveChangesAsync(cancellationToken);

        return document.Id;
    }
}