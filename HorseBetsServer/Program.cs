using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using HorseBets.Bets.Services;
using HorseBets.Data;
using HorseBetsServer.Middleware;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<IMatchService, MatchService>();
builder.Services.AddSingleton<IHorseService, HorseService>();
builder.Services.AddSingleton<IBetService, BetService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");
builder.Services.AddDbContextFactory<BetsDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
                .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                .SelectMany(x => x.Value.Errors.Select(e => new ValidationFailure(x.Key, e.ErrorMessage)))
                .ToList();
        throw new ValidationException(errors);
    };
});

builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
