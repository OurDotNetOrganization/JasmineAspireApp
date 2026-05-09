using System.Globalization;
using System.Resources;

namespace AspireApp1.Web.Resources.Strings;

public static class AppResources
{
    private static readonly Lazy<ResourceManager> Manager = new(() =>
        new ResourceManager("AspireApp1.Web.Resources.Strings.AppResources", typeof(AppResources).Assembly));

    private static string Get(string name) =>
        Manager.Value.GetString(name, CultureInfo.CurrentUICulture) ?? name;

    public static string BrandTitle => Get(nameof(BrandTitle));
    public static string NavHome => Get(nameof(NavHome));
    public static string NavJasmine => Get(nameof(NavJasmine));
    public static string NavCounter => Get(nameof(NavCounter));
    public static string NavWeather => Get(nameof(NavWeather));
    public static string AboutLink => Get(nameof(AboutLink));
    public static string UnhandledError => Get(nameof(UnhandledError));
    public static string Reload => Get(nameof(Reload));
    public static string HomePageTitle => Get(nameof(HomePageTitle));
    public static string HomeWelcome => Get(nameof(HomeWelcome));
    public static string HomeLead => Get(nameof(HomeLead));
    public static string HomeOpenProfile => Get(nameof(HomeOpenProfile));
    public static string HomeHint => Get(nameof(HomeHint));
    public static string CounterPageTitle => Get(nameof(CounterPageTitle));
    public static string ClickMe => Get(nameof(ClickMe));
    public static string WeatherPageTitle => Get(nameof(WeatherPageTitle));
    public static string WeatherDescription => Get(nameof(WeatherDescription));
    public static string Loading => Get(nameof(Loading));
    public static string Date => Get(nameof(Date));
    public static string TempC => Get(nameof(TempC));
    public static string TempF => Get(nameof(TempF));
    public static string Summary => Get(nameof(Summary));
    public static string TempCAria => Get(nameof(TempCAria));
    public static string TempFAria => Get(nameof(TempFAria));
    public static string JasminePageTitle => Get(nameof(JasminePageTitle));
    public static string SkipToMainContent => Get(nameof(SkipToMainContent));
    public static string JasmineLoadErrorNoData => Get(nameof(JasmineLoadErrorNoData));
    public static string LoadingProfile => Get(nameof(LoadingProfile));
    public static string PrimaryActions => Get(nameof(PrimaryActions));
    public static string OpenLinkedInAria => Get(nameof(OpenLinkedInAria));
    public static string MessageOnLinkedIn => Get(nameof(MessageOnLinkedIn));
    public static string WhyWorkTogether => Get(nameof(WhyWorkTogether));
    public static string PartnershipsFastest => Get(nameof(PartnershipsFastest));
    public static string FocusAreas => Get(nameof(FocusAreas));
    public static string CurrentRoles => Get(nameof(CurrentRoles));
    public static string About => Get(nameof(About));
    public static string PublicHighlights => Get(nameof(PublicHighlights));
    public static string SelectedExperience => Get(nameof(SelectedExperience));
    public static string RecommendationLinkedIn => Get(nameof(RecommendationLinkedIn));
    public static string ViewOnLinkedIn => Get(nameof(ViewOnLinkedIn));
    public static string EducationLearning => Get(nameof(EducationLearning));
    public static string CoreStrengths => Get(nameof(CoreStrengths));
    public static string OpenLinkedInProfile => Get(nameof(OpenLinkedInProfile));
    public static string SendPartnershipInquiry => Get(nameof(SendPartnershipInquiry));
    public static string Name => Get(nameof(Name));
    public static string Email => Get(nameof(Email));
    public static string OrganizationOptional => Get(nameof(OrganizationOptional));
    public static string Message => Get(nameof(Message));
    public static string Sending => Get(nameof(Sending));
    public static string SubmitInquiry => Get(nameof(SubmitInquiry));
    public static string SubmitSuccess => Get(nameof(SubmitSuccess));

    public static string FormatCurrentCount(int count) =>
        string.Format(CultureInfo.CurrentUICulture, Get("CurrentCount"), count);

    public static string FormatJasmineLoadError(string message) =>
        string.Format(CultureInfo.CurrentUICulture, Get("JasmineLoadError"), message);

    public static string FormatSubmitSuccessRef(string id) =>
        string.Format(CultureInfo.CurrentUICulture, Get("SubmitSuccessRef"), id);

    public static string FormatSubmitFailed(string message) =>
        string.Format(CultureInfo.CurrentUICulture, Get("SubmitFailed"), message);
}
