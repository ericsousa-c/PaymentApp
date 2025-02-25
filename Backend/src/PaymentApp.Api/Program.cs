using Microsoft.EntityFrameworkCore;
using PaymentApp.Application.Interfaces.Services;
using PaymentApp.Application.Services;
using PaymentApp.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PaymentAppDbContext>(options =>
    options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));

builder.Services.AddScoped<PaymentAppDbContext>();

builder.Services.AddScoped<IPagamentoService, PagamentoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
