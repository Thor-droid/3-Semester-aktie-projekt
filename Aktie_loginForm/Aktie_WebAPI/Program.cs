using Aktie_WebAPI.BusinessLogic;
using Aktie_WebAPI.DatabaseAccess;
using Aktie_WebAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<StockService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();