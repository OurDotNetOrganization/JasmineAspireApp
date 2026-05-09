using System.Security.Claims;
using System.Text;
using AspireApp1.ApiService;
using AspireApp1.ApiService.Auth;
using AspireApp1.ApiService.Contracts;
using AspireApp1.ApiService.Repositories;
using AspireApp1.ApiService.Resources.Strings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalization();

builder.Services.AddSingleton<IProfileRepository, InMemoryProfileRepository>();
builder.Services.AddSingleton<IContactLeadRepository, InMemoryContactLeadRepository>();
builder.Services.AddSingleton<JwtTokenFactory>();

var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "AspireApp1";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "AspireApp1";
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey) || jwtKey.Length < 32)
{
    throw new InvalidOperationException("Jwt:Key must be configured and at least 32 characters long.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2),
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
var supportedCultures = new[] { "en", "ar-EG" };
app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures));
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => AppResources.ApiRootStatus)
    .AllowAnonymous();

app.MapGet("/weatherforecast", () =>
{
    string[] summaries =
    [
        AppResources.WeatherFreezing,
        AppResources.WeatherBracing,
        AppResources.WeatherChilly,
        AppResources.WeatherCool,
        AppResources.WeatherMild,
        AppResources.WeatherWarm,
        AppResources.WeatherBalmy,
        AppResources.WeatherHot,
        AppResources.WeatherSweltering,
        AppResources.WeatherScorching
    ];

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.AllowAnonymous();

var api = app.MapGroup("/api");

api.MapGet("/profile/public", async (IProfileRepository repository, CancellationToken cancellationToken) =>
    TypedResults.Ok(await repository.GetPublicAsync(cancellationToken)))
    .AllowAnonymous();

api.MapPut("/profile/admin", async Task<Results<Ok, ValidationProblem>> (
        IProfileRepository repository,
        PublicProfileDto profile,
        CancellationToken cancellationToken) =>
    {
        if (!RequestValidation.TryValidateProfile(profile, out var errors))
        {
            return TypedResults.ValidationProblem(errors);
        }

        await repository.UpdateAsync(profile, cancellationToken);
        return TypedResults.Ok();
    })
    .RequireAuthorization("AdminPolicy");

api.MapPost("/contact-leads", async Task<Results<Created<ContactLeadResponseDto>, ValidationProblem>> (
        IContactLeadRepository repository,
        ContactLeadRequestDto? body,
        CancellationToken cancellationToken) =>
    {
        if (!RequestValidation.TryValidateContact(body, out var errors))
        {
            return TypedResults.ValidationProblem(errors);
        }

        var created = await repository.AddAsync(body!, cancellationToken);
        return TypedResults.Created($"/api/contact-leads/{created.Id}", created);
    })
    .AllowAnonymous();

api.MapGet("/contact-leads", async (IContactLeadRepository repository, CancellationToken cancellationToken) =>
    TypedResults.Ok(await repository.ListAsync(cancellationToken)))
    .RequireAuthorization("AdminPolicy");

api.MapPost("/auth/token", (IHostEnvironment environment, IConfiguration configuration, JwtTokenFactory tokenFactory, DevTokenRequestDto? body) =>
    {
        if (!environment.IsDevelopment())
        {
            return Results.NotFound();
        }

        if (body is null || string.IsNullOrWhiteSpace(body.Username) || string.IsNullOrWhiteSpace(body.Password))
        {
            return Results.BadRequest(AppResources.AuthUsernamePasswordRequired);
        }

        var expectedUser = configuration["Jwt:DevAdminUsername"] ?? "admin";
        var expectedPass = configuration["Jwt:DevAdminPassword"] ?? string.Empty;

        if (!string.Equals(body.Username, expectedUser, StringComparison.Ordinal)
            || !string.Equals(body.Password, expectedPass, StringComparison.Ordinal))
        {
            return Results.Unauthorized();
        }

        var lifetime = TimeSpan.FromHours(8);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, body.Username),
            new(ClaimTypes.Name, body.Username),
            new(ClaimTypes.Role, "Admin"),
        };

        var token = tokenFactory.CreateAccessToken(claims, lifetime);
        var response = new DevTokenResponseDto(token, DateTimeOffset.UtcNow.Add(lifetime));
        return Results.Ok(response);
    })
    .AllowAnonymous();

app.MapDefaultEndpoints();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
