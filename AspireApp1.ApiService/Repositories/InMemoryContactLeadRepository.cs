using System.Collections.Concurrent;
using AspireApp1.ApiService.Contracts;

namespace AspireApp1.ApiService.Repositories;

public sealed class InMemoryContactLeadRepository : IContactLeadRepository
{
    private readonly ConcurrentBag<ContactLeadItemDto> _items = [];

    public Task<ContactLeadResponseDto> AddAsync(ContactLeadRequestDto request, CancellationToken cancellationToken = default)
    {
        var id = Guid.NewGuid();
        var created = DateTimeOffset.UtcNow;
        var item = new ContactLeadItemDto(
            id,
            request.Name,
            request.Email,
            request.Organization,
            request.Message,
            created);
        _items.Add(item);
        return Task.FromResult(new ContactLeadResponseDto(id, created));
    }

    public Task<IReadOnlyList<ContactLeadItemDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        var list = _items.OrderByDescending(x => x.CreatedAtUtc).ToList();
        return Task.FromResult<IReadOnlyList<ContactLeadItemDto>>(list);
    }
}
