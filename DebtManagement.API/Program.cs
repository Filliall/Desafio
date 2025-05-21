using DebtManagement.Application.Commands;
using DebtManagement.Application.Handlers;
using DebtManagement.Application.Mapping;
using DebtManagement.Domain.Interfaces;
using DebtManagement.Infrastructure.Context;
using DebtManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDebtRepository, DebtRepository>();


builder.Services.AddDbContext<DebtContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("DebtManagement.Infrastructure") // Nome do projeto de infra
    );
});

builder.Services.AddAutoMapper(typeof(DebtProfile).Assembly);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,                     // API
        typeof(CreateDebtCommand).Assembly,            // Application
        typeof(CreateDebtCommandHandler).Assembly));

var app = builder.Build();

app.MapStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DebtContext>();
    context.Database.Migrate();
}

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Desafio API 1.0")
        .WithDownloadButton(true)
        .WithTheme(ScalarTheme.Moon)
        .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios);
});


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
