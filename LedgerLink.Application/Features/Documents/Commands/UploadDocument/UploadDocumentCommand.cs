using MediatR;

namespace LedgerLink.Application.Features.Documents.Commands.UploadDocument;

public record UploadDocumentCommand(int ServiceRequestId, string FileName, Stream FileContent, long FileSize) : IRequest<int>;