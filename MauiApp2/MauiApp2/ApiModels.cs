namespace MauiApp2;

public record ProfileLinksDto(string LinkedIn, string Optomatica, string Racemate);

public record FocusCardDto(string Title, string Body);

public record CurrentRoleDto(
    string Title,
    string CompanyName,
    string CompanyUrl,
    string CompanySubtitle,
    string DateRange,
    string Description);

public record PublicProfileDto(
    string Kicker,
    string FullName,
    string HeroParagraph,
    ProfileLinksDto Links,
    string ValueTitle,
    string ValueIntro,
    IReadOnlyList<string> ValueOutcomes,
    IReadOnlyList<FocusCardDto> FocusAreas,
    IReadOnlyList<CurrentRoleDto> CurrentRoles,
    string AboutBody,
    string AboutMetaLine,
    IReadOnlyList<string> Skills,
    string CtaTitle,
    string CtaBody);

public record ContactLeadRequestDto(string Name, string Email, string? Organization, string Message);

public record ContactLeadResponseDto(Guid Id, DateTimeOffset CreatedAtUtc);
