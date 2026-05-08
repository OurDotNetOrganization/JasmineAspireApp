using System.Text.Json;
using AspireApp1.Web.ApiModels;

namespace AspireApp1.Web;

public class ProfileApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<PublicProfileDto?> GetPublicProfileAsync(CancellationToken cancellationToken = default)
        => await httpClient.GetFromJsonAsync<PublicProfileDto>("/api/profile/public", JsonOptions, cancellationToken);
}
