using AspireApp1.ApiService.Contracts;

namespace AspireApp1.ApiService.Repositories;

public interface IContactLeadRepository
{
    Task<ContactLeadResponseDto> AddAsync(ContactLeadRequestDto request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ContactLeadItemDto>> ListAsync(CancellationToken cancellationToken = default);
}
