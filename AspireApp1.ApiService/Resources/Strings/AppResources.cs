using System.Globalization;
using System.Resources;

namespace AspireApp1.ApiService.Resources.Strings;

public static class AppResources
{
    private static readonly Lazy<ResourceManager> Manager = new(() =>
        new ResourceManager("AspireApp1.ApiService.Resources.Strings.AppResources", typeof(AppResources).Assembly));

    private static string Get(string name) =>
        Manager.Value.GetString(name, CultureInfo.CurrentUICulture) ?? name;

    public static string ApiRootStatus => Get(nameof(ApiRootStatus));
    public static string AuthUsernamePasswordRequired => Get(nameof(AuthUsernamePasswordRequired));
    public static string RequestBodyRequired => Get(nameof(RequestBodyRequired));
    public static string NameValidation => Get(nameof(NameValidation));
    public static string EmailValidation => Get(nameof(EmailValidation));
    public static string OrganizationValidation => Get(nameof(OrganizationValidation));
    public static string MessageValidation => Get(nameof(MessageValidation));
    public static string LinksValidation => Get(nameof(LinksValidation));
    public static string ValueOutcomesValidation => Get(nameof(ValueOutcomesValidation));
    public static string FocusAreasValidation => Get(nameof(FocusAreasValidation));
    public static string CurrentRolesValidation => Get(nameof(CurrentRolesValidation));
    public static string HighlightBulletsValidation => Get(nameof(HighlightBulletsValidation));
    public static string SelectedExperienceValidation => Get(nameof(SelectedExperienceValidation));
    public static string SkillsValidation => Get(nameof(SkillsValidation));
    public static string WeatherFreezing => Get(nameof(WeatherFreezing));
    public static string WeatherBracing => Get(nameof(WeatherBracing));
    public static string WeatherChilly => Get(nameof(WeatherChilly));
    public static string WeatherCool => Get(nameof(WeatherCool));
    public static string WeatherMild => Get(nameof(WeatherMild));
    public static string WeatherWarm => Get(nameof(WeatherWarm));
    public static string WeatherBalmy => Get(nameof(WeatherBalmy));
    public static string WeatherHot => Get(nameof(WeatherHot));
    public static string WeatherSweltering => Get(nameof(WeatherSweltering));
    public static string WeatherScorching => Get(nameof(WeatherScorching));

    public static string FormatFieldRequiredMax(string fieldName, int maxLen) =>
        string.Format(CultureInfo.CurrentUICulture, Get("FieldRequiredMax"), fieldName, maxLen);
}
