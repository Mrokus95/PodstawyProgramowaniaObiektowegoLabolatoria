using FluentValidation;
using FluentValidation.AspNetCore;
using PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.Services;
using PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.Validators;
using PodstawyProgramowaniaObiektowego.Modules.Fibonacci.Services;
using PodstawyProgramowaniaObiektowego.Modules.Fibonacci.Validators;
using PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Hubs;
using PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Services;
using PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5123);
});

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<ICalculateService, CalculateService>();
builder.Services.AddScoped<IMonteCarloService, MonteCarloService>();
builder.Services.AddScoped<IFibonacciService, FibonacciService>();

builder.Services.AddValidatorsFromAssemblyContaining<GetAmountInCurrencyValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SimulationRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<FibonacciRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.SetIsOriginAllowed(origin => true) 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.MapControllers();
app.MapHub<MonteCarloHub>("/monteCarloHub");

app.Run();