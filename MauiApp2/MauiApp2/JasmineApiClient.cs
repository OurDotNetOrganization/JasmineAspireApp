using System.Net.Http.Json;
using System.Text.Json;

namespace MauiApp2;

public sealed class JasmineApiClient
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;

    public JasmineApiClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(GetApiBaseUrl()),
            Timeout = TimeSpan.FromSeconds(20),
        };
    }

    public Task<PublicProfileDto?> GetProfileAsync(CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<PublicProfileDto>("/api/profile/public", JsonOptions, cancellationToken);

    public async Task<ContactLeadResponseDto?> SubmitLeadAsync(ContactLeadRequestDto request, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync("/api/contact-leads", request, JsonOptions, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ContactLeadResponseDto>(JsonOptions, cancellationToken);
    }

    private static string GetApiBaseUrl()
    {
#if ANDROID
        return "http://10.0.2.2:5389";
#else
        return "http://localhost:5389";
#endif
    }
}
