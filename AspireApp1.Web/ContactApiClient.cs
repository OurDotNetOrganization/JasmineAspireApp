using System.Net.Http.Json;
using System.Text.Json;
using AspireApp1.Web.ApiModels;

namespace AspireApp1.Web;

public class ContactApiClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<ContactLeadResponseDto?> SubmitLeadAsync(ContactLeadRequestDto request, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync("/api/contact-leads", request, JsonOptions, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ContactLeadResponseDto>(JsonOptions, cancellationToken);
    }
}
