using MediatR;

namespace LedgerLink.Application.Features.Documents.Queries.GetDocument;

public record GetDocumentQuery(int DocumentId) : IRequest<FileDownloadDto>;