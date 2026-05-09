using System.Globalization;
using System.Resources;

namespace MauiApp2.Resources.Strings;

public static class AppResources
{
    private static readonly Lazy<ResourceManager> Manager = new(() =>
        new ResourceManager("MauiApp2.Resources.Strings.AppResources", typeof(AppResources).Assembly));

    private static string Get(string name) =>
        Manager.Value.GetString(name, CultureInfo.CurrentUICulture) ?? name;

    public static string AppShellTitle => Get(nameof(AppShellTitle));
    public static string HomeTabTitle => Get(nameof(HomeTabTitle));
    public static string PageTitle => Get(nameof(PageTitle));
    public static string LoadingProfile => Get(nameof(LoadingProfile));
    public static string ApiNotConfigured => Get(nameof(ApiNotConfigured));
    public static string ProfileNoData => Get(nameof(ProfileNoData));
    public static string ProfileLoaded => Get(nameof(ProfileLoaded));
    public static string ContactInquiry => Get(nameof(ContactInquiry));
    public static string YourName => Get(nameof(YourName));
    public static string YourEmail => Get(nameof(YourEmail));
    public static string OrganizationOptional => Get(nameof(OrganizationOptional));
    public static string MessagePlaceholder => Get(nameof(MessagePlaceholder));
    public static string SubmitInquiry => Get(nameof(SubmitInquiry));
    public static string ValidationMessage => Get(nameof(ValidationMessage));
    public static string SubmittingInquiry => Get(nameof(SubmittingInquiry));
    public static string InquirySubmitted => Get(nameof(InquirySubmitted));
    public static string Ready => Get(nameof(Ready));
    public static string SubmitFailed => Get(nameof(SubmitFailed));

    public static string FormatProfileLoadFailed(string message) =>
        string.Format(CultureInfo.CurrentUICulture, Get("ProfileLoadFailed"), message);

    public static string FormatInquirySubmittedRef(string reference) =>
        string.Format(CultureInfo.CurrentUICulture, Get("InquirySubmittedRef"), reference);

    public static string FormatSubmitFailedMessage(string message) =>
        string.Format(CultureInfo.CurrentUICulture, Get("SubmitFailedMessage"), message);
}
