using AspireApp1.ApiService.Contracts;

namespace AspireApp1.ApiService.Repositories;

public interface IProfileRepository
{
    Task<PublicProfileDto> GetPublicAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(PublicProfileDto profile, CancellationToken cancellationToken = default);
}
