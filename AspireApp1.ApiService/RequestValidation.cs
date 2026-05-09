using System.Net.Mail;
using AspireApp1.ApiService.Contracts;
using AspireApp1.ApiService.Resources.Strings;

namespace AspireApp1.ApiService;

public static class RequestValidation
{
    public static bool TryValidateContact(ContactLeadRequestDto? dto, out Dictionary<string, string[]> errors)
    {
        errors = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

        if (dto is null)
        {
            errors.Add("body", [AppResources.RequestBodyRequired]);
            return false;
        }

        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Trim().Length > 120)
        {
            errors.Add(nameof(dto.Name), [AppResources.NameValidation]);
        }

        if (string.IsNullOrWhiteSpace(dto.Email) || dto.Email.Length > 256 || !IsValidEmail(dto.Email))
        {
            errors.Add(nameof(dto.Email), [AppResources.EmailValidation]);
        }

        if (dto.Organization is not null && dto.Organization.Length > 200)
        {
            errors.Add(nameof(dto.Organization), [AppResources.OrganizationValidation]);
        }

        if (string.IsNullOrWhiteSpace(dto.Message) || dto.Message.Length < 10 || dto.Message.Length > 4000)
        {
            errors.Add(nameof(dto.Message), [AppResources.MessageValidation]);
        }

        return errors.Count == 0;
    }

    public static bool TryValidateProfile(PublicProfileDto? dto, out Dictionary<string, string[]> errors)
    {
        var problems = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

        if (dto is null)
        {
            problems.Add("body", [AppResources.RequestBodyRequired]);
            errors = problems;
            return false;
        }

        void RequireNonEmpty(string fieldName, string? value, int maxLen)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > maxLen)
            {
                problems.Add(fieldName, [AppResources.FormatFieldRequiredMax(fieldName, maxLen)]);
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
            problems.Add(nameof(dto.Links), [AppResources.LinksValidation]);
        }

        if (dto.ValueOutcomes is null || dto.ValueOutcomes.Count is 0 or > 20)
        {
            problems.Add(nameof(dto.ValueOutcomes), [AppResources.ValueOutcomesValidation]);
        }

        if (dto.FocusAreas is null || dto.FocusAreas.Count is 0 or > 20)
        {
            problems.Add(nameof(dto.FocusAreas), [AppResources.FocusAreasValidation]);
        }

        if (dto.CurrentRoles is null || dto.CurrentRoles.Count is 0 or > 20)
        {
            problems.Add(nameof(dto.CurrentRoles), [AppResources.CurrentRolesValidation]);
        }

        if (dto.HighlightBullets is null || dto.HighlightBullets.Count is 0 or > 50)
        {
            problems.Add(nameof(dto.HighlightBullets), [AppResources.HighlightBulletsValidation]);
        }

        if (dto.SelectedExperience is null || dto.SelectedExperience.Count is 0 or > 50)
        {
            problems.Add(nameof(dto.SelectedExperience), [AppResources.SelectedExperienceValidation]);
        }

        if (dto.Skills is null || dto.Skills.Count is 0 or > 100)
        {
            problems.Add(nameof(dto.Skills), [AppResources.SkillsValidation]);
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
