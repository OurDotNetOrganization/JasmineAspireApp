using Microsoft.Extensions.DependencyInjection;
using MauiApp2.Resources.Strings;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
    private readonly JasmineApiClient? _apiClient;
    private PublicProfileDto? _profile;

    public MainPage()
    {
        InitializeComponent();
        _apiClient = IPlatformApplication.Current?.Services.GetService<JasmineApiClient>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadProfileAsync();
    }

    private async Task LoadProfileAsync()
    {
        try
        {
            SubmitResultLabel.Text = string.Empty;
            StatusLabel.Text = AppResources.LoadingProfile;
            if (_apiClient is null)
            {
                StatusLabel.Text = AppResources.ApiNotConfigured;
                return;
            }
            _profile = await _apiClient.GetProfileAsync();
            if (_profile is null)
            {
                StatusLabel.Text = AppResources.ProfileNoData;
                return;
            }

            NameLabel.Text = _profile.FullName;
            KickerLabel.Text = _profile.Kicker;
            HeroLabel.Text = _profile.HeroParagraph;
            ValueTitleLabel.Text = _profile.ValueTitle;
            ValueIntroLabel.Text = _profile.ValueIntro;
            OutcomesCollection.ItemsSource = _profile.ValueOutcomes;
            StatusLabel.Text = AppResources.ProfileLoaded;
        }
        catch (Exception ex)
        {
            StatusLabel.Text = AppResources.FormatProfileLoadFailed(ex.Message);
        }
    }

    private async void OnSubmitClicked(object? sender, EventArgs e)
    {
        SubmitResultLabel.Text = string.Empty;

        var name = NameEntry.Text?.Trim() ?? string.Empty;
        var email = EmailEntry.Text?.Trim() ?? string.Empty;
        var organization = string.IsNullOrWhiteSpace(OrganizationEntry.Text) ? null : OrganizationEntry.Text.Trim();
        var message = MessageEditor.Text?.Trim() ?? string.Empty;

        if (name.Length == 0 || email.Length == 0 || message.Length < 10)
        {
            SubmitResultLabel.TextColor = Colors.IndianRed;
            SubmitResultLabel.Text = AppResources.ValidationMessage;
            return;
        }

        try
        {
            if (_apiClient is null)
            {
                SubmitResultLabel.TextColor = Colors.IndianRed;
                SubmitResultLabel.Text = AppResources.ApiNotConfigured;
                return;
            }
            SubmitButton.IsEnabled = false;
            StatusLabel.Text = AppResources.SubmittingInquiry;

            var response = await _apiClient.SubmitLeadAsync(new ContactLeadRequestDto(name, email, organization, message));
            SubmitResultLabel.TextColor = Colors.SeaGreen;
            SubmitResultLabel.Text = response is null
                ? AppResources.InquirySubmitted
                : AppResources.FormatInquirySubmittedRef(response.Id.ToString("N"));

            NameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            OrganizationEntry.Text = string.Empty;
            MessageEditor.Text = string.Empty;
            StatusLabel.Text = AppResources.Ready;
        }
        catch (Exception ex)
        {
            SubmitResultLabel.TextColor = Colors.IndianRed;
            SubmitResultLabel.Text = AppResources.FormatSubmitFailedMessage(ex.Message);
            StatusLabel.Text = AppResources.SubmitFailed;
        }
        finally
        {
            SubmitButton.IsEnabled = true;
        }
    }
}
