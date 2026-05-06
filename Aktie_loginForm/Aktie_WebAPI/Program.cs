using Aktie_WebAPI.BusinessLogic;
using Aktie_WebAPI.DatabaseAccess;
using Aktie_WebAPI.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NotifAccess>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<StockService>();
builder.Services.AddScoped<AuthLogic>();
builder.Services.AddScoped<AuthAccess>();
builder.Services.AddScoped<AbonnementLogic>();
builder.Services.AddScoped<AbonnementAccess>();

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