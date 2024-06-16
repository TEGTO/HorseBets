using AspireRedisTest.WebAppAuthorization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Fluxor;
using HorseBets.Bets.Services;
using HorseBets.Bets.Services.Api;
using HorseBets.Components;
using HorseBets.Components.Account;
using HorseBets.Data;
using HorseBets.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddRedisOutputCache("cache");
builder.Services.AddOutputCache(options =>
{
    options.AddPolicy(nameof(AuthCachePolicy), AuthCachePolicy.Instance);
});

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<UserManager<ApplicationUser>, IdentityUserManager>();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddHttpClient("bets", (httpClient) =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetSection("BetsAPI").Value!);
});
builder.Services.AddScoped<RoleManager>();
builder.Services.AddScoped<IClientApi, ClientApi>();
builder.Services.AddScoped<IMatchApi, MatchApi>();
builder.Services.AddScoped<IHorseApi, HorseApi>();
builder.Services.AddScoped<IBetApi, BetApi>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IBetService, BetService>();

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddBlazorBootstrap();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

ValidatorOptions.Global.LanguageManager.Enabled = false;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();
builder.Services.AddHttpContextAccessor();

var currentAssembly = typeof(Program).Assembly;
builder.Services.AddFluxor(options =>
    options.ScanAssemblies(currentAssembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.CreateDbIfNotExists();
}

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
    app.UseMigrationsEndPoint();
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseOutputCache();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();
