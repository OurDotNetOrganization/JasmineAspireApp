using System.Net.Mail;
using AspireApp1.ApiService.Contracts;

namespace AspireApp1.ApiService;

public static class RequestValidation
{
    public static bool TryValidateContact(ContactLeadRequestDto? dto, out Dictionary<string, string[]> errors)
    {
        errors = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

        if (dto is null)
        {
            errors.Add("body", ["Request body is required."]);
            return false;
        }

        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Trim().Length > 120)
        {
            errors.Add(nameof(dto.Name), ["Name is required and must be 120 characters or fewer."]);
        }

        if (string.IsNullOrWhiteSpace(dto.Email) || dto.Email.Length > 256 || !IsValidEmail(dto.Email))
        {
            errors.Add(nameof(dto.Email), ["A valid email is required (256 characters or fewer)."]);
        }

        if (dto.Organization is not null && dto.Organization.Length > 200)
        {
            errors.Add(nameof(dto.Organization), ["Organization must be 200 characters or fewer."]);
        }

        if (string.IsNullOrWhiteSpace(dto.Message) || dto.Message.Length < 10 || dto.Message.Length > 4000)
        {
            errors.Add(nameof(dto.Message), ["Message must be between 10 and 4000 characters."]);
        }

        return errors.Count == 0;
    }

    public static bool TryValidateProfile(PublicProfileDto? dto, out Dictionary<string, string[]> errors)
    {
        var problems = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

        if (dto is null)
        {
            problems.Add("body", ["Request body is required."]);
            errors = problems;
            return false;
        }

        void RequireNonEmpty(string fieldName, string? value, int maxLen)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > maxLen)
            {
                problems.Add(fieldName, [$"{fieldName} is required and must be {maxLen} characters or fewer."]);
            }
        }

        RequireNonEmpty(nameof(dto.Kicker), dto.Kicker, 200);
        RequireNonEmpty(nameof(dto.FullName), dto.FullName, 120);
        RequireNonEmpty(nameof(dto.HeroParagraph), dto.HeroParagraph, 2000);
        RequireNonEmpty(nameof(dto.ValueTitle), dto.ValueTitle, 200);
        RequireNonEmpty(nameof(dto.ValueIntro), dto.ValueIntro, 2000);
        RequireNonEmpty(nameof(dto.AboutBody), dto.AboutBody, 4000);
        RequireNonEmpty(nameof(dto.AboutMetaLine), dto.AboutMetaLine, 500);
        RequireNonEmpty(nameof(dto.HighlightsIntro), dto.HighlightsIntro, 1000);
        RequireNonEmpty(nameof(dto.TestimonialQuote), dto.TestimonialQuote, 4000);
        RequireNonEmpty(nameof(dto.TestimonialAttribution), dto.TestimonialAttribution, 500);
        RequireNonEmpty(nameof(dto.EducationPrimary), dto.EducationPrimary, 500);
        RequireNonEmpty(nameof(dto.EducationFootnote), dto.EducationFootnote, 2000);
        RequireNonEmpty(nameof(dto.CtaTitle), dto.CtaTitle, 200);
        RequireNonEmpty(nameof(dto.CtaBody), dto.CtaBody, 2000);
        RequireNonEmpty(nameof(dto.FooterNote), dto.FooterNote, 2000);

        if (dto.Links is null
            || !IsAbsoluteUrl(dto.Links.LinkedIn)
            || !IsAbsoluteUrl(dto.Links.Optomatica)
            || !IsAbsoluteUrl(dto.Links.Racemate))
        {
            problems.Add(nameof(dto.Links), ["Links must include valid absolute URLs for LinkedIn, Optomatica, and Racemate."]);
        }

        if (dto.ValueOutcomes is null || dto.ValueOutcomes.Count is 0 or > 20)
        {
            problems.Add(nameof(dto.ValueOutcomes), ["ValueOutcomes must contain between 1 and 20 items."]);
        }

        if (dto.FocusAreas is null || dto.FocusAreas.Count is 0 or > 20)
        {
            problems.Add(nameof(dto.FocusAreas), ["FocusAreas must contain between 1 and 20 items."]);
        }

        if (dto.CurrentRoles is null || dto.CurrentRoles.Count is 0 or > 20)
        {
            problems.Add(nameof(dto.CurrentRoles), ["CurrentRoles must contain between 1 and 20 items."]);
        }

        if (dto.HighlightBullets is null || dto.HighlightBullets.Count is 0 or > 50)
        {
            problems.Add(nameof(dto.HighlightBullets), ["HighlightBullets must contain between 1 and 50 items."]);
        }

        if (dto.SelectedExperience is null || dto.SelectedExperience.Count is 0 or > 50)
        {
            problems.Add(nameof(dto.SelectedExperience), ["SelectedExperience must contain between 1 and 50 items."]);
        }

        if (dto.Skills is null || dto.Skills.Count is 0 or > 100)
        {
            problems.Add(nameof(dto.Skills), ["Skills must contain between 1 and 100 items."]);
        }

        errors = problems;
        return problems.Count == 0;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            _ = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsAbsoluteUrl(string url)
        => Uri.TryCreate(url, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
}
