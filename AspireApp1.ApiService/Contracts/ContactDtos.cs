namespace AspireApp1.ApiService.Contracts;

public record ContactLeadRequestDto(
    string Name,
    string Email,
    string? Organization,
    string Message);

public record ContactLeadResponseDto(Guid Id, DateTimeOffset CreatedAtUtc);

public record ContactLeadItemDto(
    Guid Id,
    string Name,
    string Email,
    string? Organization,
    string Message,
    DateTimeOffset CreatedAtUtc);

public record DevTokenRequestDto(string Username, string Password);

public record DevTokenResponseDto(string AccessToken, DateTimeOffset ExpiresAtUtc);
